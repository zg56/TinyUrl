using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyUrlService.Clients.Interfaces;
using TinyUrlService.Services.Interfaces;
using Microsoft.Extensions.Logging;
using TinyUrlService.Services;

namespace TinyUrlService.Clients
{
    public class TinyUrlClient : ITinyUrlClient
    {
        private readonly ILogger<TinyUrlClient> _logger;
        private readonly IUrlShortenerService _tinyUrlService;
        private readonly IUserInput _userInput;

        public TinyUrlClient(ILogger<TinyUrlClient> logger, IUrlShortenerService tinyUrlService, IUserInput userInput)
        {
            _tinyUrlService = tinyUrlService;
            _logger = logger;
            _userInput = userInput;
        }
        public async Task CreateShortUrl()
        {
            var longUrl = _userInput.GetInput("Enter Long URL: ");
            var customShortUrl = _userInput.GetInput("Enter Custom Short URL (optional): ");

            try
            {
                var shortUrl = _tinyUrlService.CreateShortUrl(longUrl, string.IsNullOrEmpty(customShortUrl) ? null : customShortUrl);
                string output = $"Short URL created: {shortUrl}";
                if (_userInput is AutomatedUserInput autoInput) autoInput.AddOutput(output);

                Console.WriteLine(output);

            }
            catch (Exception ex)
            {
                await LogError($"Error: {ex.Message}");
            }
        }

        public async Task DeleteShortUrl()
        {
            var shortUrl = _userInput.GetInput("Enter Short URL to delete: ");

            try
            {
                _tinyUrlService.DeleteShortUrl(shortUrl);
                string output = "Short URL deleted.";
                if (_userInput is AutomatedUserInput autoInput) autoInput.AddOutput(output);

                Console.WriteLine(output);
            }
            catch (Exception ex)
            {
                await LogError($"Error: {ex.Message}");
            }
        }

        public async Task GetLongUrlByShortUrl()
        {
            var shortUrl = _userInput.GetInput("Enter Short URL: ");

            try
            {
                var longUrl = _tinyUrlService.GetLongUrlByShortUrl(shortUrl);
                string output = $"Long URL: {longUrl}";
                if (_userInput is AutomatedUserInput autoInput) autoInput.AddOutput(output);

                Console.WriteLine(output);
            }
            catch (Exception ex)
            {
                await LogError($"Error: {ex.Message}");
            }
        }

        public async Task GetStatisticsByShortUrl()
        {
            var shortUrl = _userInput.GetInput("Enter Short URL: ");

            try
            {
                var count = _tinyUrlService.GetStatisticsByShortUrl(shortUrl);
                string output = $"Access Count: {count}";
                if (_userInput is AutomatedUserInput autoInput) autoInput.AddOutput(output);

                Console.WriteLine(output);
            }
            catch (Exception ex)
            {
                await LogError($"Error: {ex.Message}");
            }
        }

        // Setting it up as async for more involved future logging like Datadog
        private async Task LogError(string message)
        {
            await Console.Out.WriteLineAsync("------------------");
            await Console.Out.WriteLineAsync(message);
            await Console.Out.FlushAsync();
        }
    }
}
