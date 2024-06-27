using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyUrlService.Models
{
    public class UrlMapping
    {
        public string LongUrl { get; set; } = "";
        public string ShortUrl { get; set; } = "";
        public int AccessCount { get; set; }
    }
}
