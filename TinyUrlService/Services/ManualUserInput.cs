using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyUrlService.Services.Interfaces;

namespace TinyUrlService.Services
{
    public class ManualUserInput : IUserInput
    {
        public string GetInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
