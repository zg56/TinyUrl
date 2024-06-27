using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyUrlService.Clients.Interfaces
{
    public interface ITinyUrlClient
    {
        Task CreateShortUrl();
        Task DeleteShortUrl();
        Task GetLongUrlByShortUrl();
        Task GetStatisticsByShortUrl();
    }
}
