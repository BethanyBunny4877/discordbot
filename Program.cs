using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ConsoleApp3.Services;


namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();


            var services = ConfigureServices();
            services.GetRequiredService<LogService>();
            await services.GetRequiredService<CommandHandlingService>().InitializeAsync(services);
            await services.GetRequiredService<ReactionHandlingModule>().InitializeAsync(services);

            await _client.LoginAsync(TokenType.Bot, "");
            await _client.StartAsync();
            Console.WriteLine("###################################################################");
            Console.WriteLine("# 00         000000  00      00     0000     0000000   00000000   #");
            Console.WriteLine("# 00           00     00    00      00  00   00   00      00      #");
            Console.WriteLine("# 00           00      00  00       0000     00   00      00      #");
            Console.WriteLine("# 00           00       0000        00  00   00   00      00      #");
            Console.WriteLine("# 000000     000000      00         0000     0000000      00      #");
            Console.WriteLine("###################################################################");
            await Task.Delay(-1);
            

        }

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                // Base
                .AddSingleton(_client)
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .AddSingleton<ReactionHandlingModule>()
                // Logging
                .AddLogging()
                .AddSingleton<LogService>()
                // Extra
                // Add additional services here...
                .BuildServiceProvider();
        }
    }
}
    

