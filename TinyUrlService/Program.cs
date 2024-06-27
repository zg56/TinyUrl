using System;
using Microsoft.Extensions.DependencyInjection;
using TinyUrlService;
using TinyUrlService.Clients.Interfaces;
using TinyUrlService.Services;
using TinyUrlService.Services.Interfaces;

public class Program
{
    private static ServiceProvider serviceProvider;

    public static async Task Main(string[] args)
    {
        // Ask user for mode
        Console.WriteLine("Select mode:");
        Console.WriteLine("1. Manual");
        Console.WriteLine("2. Automated");
        Console.Write("Select an option: ");
        var mode = Console.ReadLine();

        var isAutomated = mode == "2";
        TestCase testCase = isAutomated ? AutomatedTestCases.GetTestCase() : null;

        var serviceCollection = new ServiceCollection();
        var startup = new Startup();
        startup.ConfigureServices(serviceCollection, isAutomated, testCase);
        serviceProvider = serviceCollection.BuildServiceProvider();

        var _tinyUrlClient = serviceProvider.GetService<ITinyUrlClient>();

        var _userInputService = serviceProvider.GetService<IUserInput>();


        while (true)
        {
            Console.WriteLine("--------Tiny URL POC--------");
            Console.WriteLine("Press 1 to create a short URL");
            Console.WriteLine("Press 2 to delete a short URL");
            Console.WriteLine("Press 3 to get the long URL mapped to the short URL");
            Console.WriteLine("Press 4 to see statistics on how many times a short URL was clicked");
            Console.WriteLine("Press 5 to exit");
            Console.WriteLine("Select an option: ");

            var option = _userInputService.GetInput(string.Empty);

            switch (option)
            {
                case "1":
                    await _tinyUrlClient.CreateShortUrl();
                    break;
                case "2":
                    await _tinyUrlClient.DeleteShortUrl();
                    break;
                case "3":
                    await _tinyUrlClient.GetLongUrlByShortUrl();
                    break;
                case "4":
                    await _tinyUrlClient.GetStatisticsByShortUrl();
                    break;
                case "5":
                    if (mode == "2" && _userInputService is AutomatedUserInput automatedUserInput)
                    {

                        var result = automatedUserInput.ValidateResults();

                        if (result)
                        {
                            Console.WriteLine("Automated test passed.");
                        }
                        else
                        {
                            Console.WriteLine("Automated test failed.");
                        }

                    }
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
