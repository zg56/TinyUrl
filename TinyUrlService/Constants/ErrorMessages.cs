using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyUrlService.Constants
{
    public class ErrorMessages
    {
        public static string LongUrlNull => "Long URL cannot be null or empty.";
        public static string LongUrlNotValidFormat => "Input URL is not a valid URL! Ensure it's a valid http or https URL";
        public static string ShortUrlAlreadyExists => "Custom short URL already exists.";
        public static string FailedToAddToDictionary => "Failed to add the short URL to the dictionary.";
        public static string ShortUrlNull => "Short URL cannot be null or empty.";
        public static string ShortUrlNotPresent => "Short URL does not exist.";

    }
}
