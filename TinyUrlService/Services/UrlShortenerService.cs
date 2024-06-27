using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TinyUrlService.Constants;
using TinyUrlService.Models;
using TinyUrlService.Services.Interfaces;


namespace TinyUrlService.Services
{
    public class UrlShortenerService : IUrlShortenerService
    {
        private readonly ConcurrentDictionary<string, UrlMapping> urlMap = new();
        private const int ShortUrlLength = 8;

        public string CreateShortUrl(string longUrl, string customShortUrl = null)
        {
            if (string.IsNullOrEmpty(longUrl))
            {
                throw new ArgumentException(ErrorMessages.LongUrlNull, nameof(longUrl));
            }

            Uri uriResult;
            bool result = Uri.TryCreate(longUrl, UriKind.Absolute, out uriResult) &&
               (uriResult?.Scheme == Uri.UriSchemeHttp || uriResult?.Scheme == Uri.UriSchemeHttps);

            if (!result)
            {
                throw new ArgumentException(ErrorMessages.LongUrlNotValidFormat, nameof(longUrl));
            }

            string host = uriResult.GetLeftPart(UriPartial.Authority);

            string shortUrl;
            if (!string.IsNullOrEmpty(customShortUrl))
            {
                shortUrl = $"{host}/{customShortUrl}";

                if (urlMap.ContainsKey(shortUrl))
                    throw new InvalidOperationException(ErrorMessages.ShortUrlAlreadyExists);
            }
            else
            {
                shortUrl = GenerateShortUrlFromHash(longUrl);
                shortUrl = $"{host}/{shortUrl}";
                // in case a short URL would already exists
                while (urlMap.ContainsKey(shortUrl))
                {
                    longUrl += Guid.NewGuid().ToString();
                    shortUrl = GenerateShortUrlFromHash(longUrl);
                    shortUrl = $"{host}/{shortUrl}";
                }
            }

            var urlMapping = new UrlMapping
            {
                LongUrl = longUrl,
                ShortUrl = shortUrl,
                AccessCount = 0
            };

            if (!urlMap.TryAdd(shortUrl, urlMapping))
            {
                throw new InvalidOperationException(ErrorMessages.FailedToAddToDictionary);
            }

            return shortUrl;
        }

        public void DeleteShortUrl(string shortUrl)
        {
            if (string.IsNullOrWhiteSpace(shortUrl))
            {
                throw new ArgumentException(ErrorMessages.ShortUrlNull, nameof(shortUrl));
            }

            if (!urlMap.TryRemove(shortUrl, out _))
            {
                throw new ArgumentException(ErrorMessages.ShortUrlNotPresent, nameof(shortUrl));
            }
        }

        public string GetLongUrlByShortUrl(string shortUrl)
        {
            if (string.IsNullOrWhiteSpace(shortUrl))
            {
                throw new ArgumentException(ErrorMessages.ShortUrlNull, nameof(shortUrl));
            }

            if (urlMap.TryGetValue(shortUrl, out var urlMapping))
            {
                urlMap.AddOrUpdate(shortUrl, urlMapping, (key, existingVal) =>
                {
                    existingVal.AccessCount++;
                    return existingVal;
                });

                return urlMapping.LongUrl;
            }
            throw new InvalidOperationException(ErrorMessages.ShortUrlNotPresent);
        }

        public int GetStatisticsByShortUrl(string shortUrl)
        {
            if (string.IsNullOrWhiteSpace(shortUrl))
            {
                throw new ArgumentException(ErrorMessages.ShortUrlNull, nameof(shortUrl));
            }

            if (urlMap.TryGetValue(shortUrl, out var urlMapping))
            {
                return urlMapping.AccessCount;
            }

            throw new InvalidOperationException(ErrorMessages.ShortUrlNotPresent);
        }

        private string GenerateShortUrlFromHash(string longUrl)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(longUrl));
                var base64Hash = Convert.ToBase64String(hashBytes);
                return base64Hash.Replace('+', '-').Replace('/', '_').Substring(0, ShortUrlLength);
            }
        }
    }
}
