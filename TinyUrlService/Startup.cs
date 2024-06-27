using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TinyUrlService.Clients;
using TinyUrlService.Clients.Interfaces;
using TinyUrlService.Services;
using TinyUrlService.Services.Interfaces;

namespace TinyUrlService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services, bool isAutomated, TestCase testCase = null)
        {
            services.AddSingleton<IUrlShortenerService, UrlShortenerService>();
            services.AddSingleton<ITinyUrlClient, TinyUrlClient>();
            services.AddLogging(configure => configure.AddConsole());

            if (isAutomated)
            {
                services.AddSingleton<IUserInput>(new AutomatedUserInput(testCase.Inputs, testCase.ExpectedResults, Console.Write));
            }
            else
            {
                services.AddSingleton<IUserInput, ManualUserInput>();
            }
        }
    }
}
