using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyUrlService
{
    public class TestCase
    {
        public IEnumerable<string> Inputs { get; set; }
        public Dictionary<string, string> ExpectedResults { get; set; }
    }

    public static class AutomatedTestCases
    {
        public static TestCase GetTestCase()
        {
            return new TestCase
            {
                Inputs = new List<string>
                {
                    "1", // Create Short URL
                    "https://www.google.com/maps/place/United+States/@44.2423649,-119.8093025,3z/data=!3m1!4b1!4m6!3m5!1s0x54eab584e432360b:0x1c3bb99243deb742!8m2!3d37.09024!4d-95.712891!16zL20vMDljN3cw?entry=ttu", // Long URL
                    "", // No custom short URL set, let the hash do it
                    "1", // Create Short URL
                    "https://www.google.com/maps/place/New+York,+NY/@40.6970193,-74.3093187,10z/data=!3m1!4b1!4m6!3m5!1s0x89c24fa5d33f083b:0xc80b8f06e177fe62!8m2!3d40.7127753!4d-74.0059728!16zL20vMDJfMjg2?entry=ttu", // Long URL
                    "custom", // Set custom short URL set
                    "3", // Get Long URL by short URL
                    "https://www.google.com/custom",
                    "3", // Get Long URL by short URL
                    "https://www.google.com/5LBL1S8u",                    
                    "3", // Get Long URL by short URL
                    "https://www.google.com/custom",                    
                    "3", // Get Long URL by short URL
                    "https://www.google.com/5LBL1S8u",
                    "3", // Get Long URL by short URL
                    "https://example.com/custom", // Custom short URL -- does not exist
                    "3", // Get Long URL by short URL
                    "https://www.google.com/custom", // Custom short URL
                    "4", // Get statistics
                    "https://www.google.com/custom", // Custom short URL
                    "4", // Get statistics
                    "https://www.google.com/5LBL1S8u", // Custom short URL
                    "5" // Exit
                },
                ExpectedResults = new Dictionary<string, string>
                {
                    { "Long URL", "https://www.google.com/maps/place/United+States/@44.2423649,-119.8093025,3z/data=!3m1!4b1!4m6!3m5!1s0x54eab584e432360b:0x1c3bb99243deb742!8m2!3d37.09024!4d-95.712891!16zL20vMDljN3cw?entry=ttu" }
                }
            };
        }
    }
}
