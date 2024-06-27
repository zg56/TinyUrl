using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyUrlService.Services.Interfaces
{
    public interface IUserInput
    {
        string GetInput(string prompt);
    }
}
