using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyUrlService.Services.Interfaces
{
    public interface IUrlShortenerService
    {
        string CreateShortUrl(string longUrl, string customShortUrl = null);
        void DeleteShortUrl(string shortUrl);
        string GetLongUrlByShortUrl(string shortUrl);
        int GetStatisticsByShortUrl(string shortUrl);
    }
}
