using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleApp3.Services
{
    public class CommandHandlingService
    {
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private IServiceProvider _provider;
        string[] blockedwords;

        public CommandHandlingService(IServiceProvider provider, DiscordSocketClient discord, CommandService commands)
        {
            _discord = discord;
            _commands = commands;
            _provider = provider;
            _discord.MessageReceived += MessageReceived;
        }
        public async Task InitializeAsync(IServiceProvider provider)
        {
            _provider = provider;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), services: null);
            // Add additional initialization code here...
            var con = new SqlConnection("Data Source=regigigas.database.windows.net;Initial Catalog=BadWords;Integrated Security=false;User ID=RetroGamerSP;Password=Herobrine4009");
            string sql = @"SELECT word
               FROM  dbo.BarredWords";
            using (var command = new SqlCommand(sql, con))
            {
                con.Open();
                using (var reader = command.ExecuteReader())
                {
                    var list = new List<string>();
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0));
                    }
                    blockedwords = list.ToArray();
                }
            }
        }


        private async Task MessageReceived(SocketMessage rawMessage)
        {
            // Ignore system messages and messages from bots
            if (!(rawMessage is SocketUserMessage message)) return;
            if (message.Source != MessageSource.User) return;

            int argPos = 0;
            if (message.HasCharPrefix('!', ref argPos))
            {
                var context = new SocketCommandContext(_discord, message);
                var result = await _commands.ExecuteAsync(context, argPos, _provider);

                if (result.Error.HasValue && result.Error.Value != CommandError.UnknownCommand)
                {
                    await context.Channel.SendMessageAsync(result.ToString());
                }
                else if (result.IsSuccess)
                {
                    Discord.IUser U = context.Message.Author;
                    Console.WriteLine($"[General/Command] {DateTime.Now.ToLongTimeString()} Command     {U} has used command {message.ToString()}");
                }
            }
            else

            {
     
                var con = new SqlConnection("Data Source=regigigas.database.windows.net;Initial Catalog=BadWords;Integrated Security=false;User ID=RetroGamerSP;Password=Herobrine4009");
                string sql = @"SELECT word
               FROM  dbo.BarredWords";
                using (var command = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        var list = new List<string>();
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(0));
                        }
                        blockedwords = list.ToArray();
                    }
                }
                string stringtocheck = message.ToString();
                foreach (string x in blockedwords)
                {
                    string hopattern = @"\bho-oh\b";
                    string hopattern2 = @"\bho oh\b";
                    string pattern = @"\b" + x + @"\b";
                    if (Regex.IsMatch(stringtocheck, pattern, RegexOptions.IgnoreCase) && !Regex.IsMatch(stringtocheck, hopattern, RegexOptions.IgnoreCase) && !Regex.IsMatch(stringtocheck, hopattern2, RegexOptions.IgnoreCase))
                    {
                        await message.DeleteAsync();
                        await message.Author.SendMessageAsync("You used a prohibited word || " + x);
                        Discord.IUser E = message.Author;
                        var _guild = message.Channel as SocketGuildChannel;
                        var _client = _guild.Guild;
                        string imgurl = E.AvatarId;
                        string userurl = E.Id.ToString();
                        string AvatartoEmbed = $"https://cdn.discordapp.com/avatars/{userurl}/{imgurl}.webp?size=1024";
                        string PunishmentType = "Deleted Message";
                        ulong ID = 677610610090835978;
                        var channel = _client.GetChannel(ID) as IMessageChannel;
                        var EmbedAuth = new EmbedAuthorBuilder
                        {
                            Name = E.ToString(),

                        };

                            var builder = new EmbedBuilder()
                                .WithTitle($"Punishment Type: {PunishmentType}")
                                .WithDescription($"Punished User: {E.Username.ToString()}")
                                .WithUrl("https://discordapp.com")
                                .WithTimestamp(DateTimeOffset.FromUnixTimeMilliseconds(1580207925966))
                                .WithFooter(footer =>
                                {
                                    footer
                                        .WithText("Punished On");
                                })
                                .WithImageUrl(AvatartoEmbed)
                                .WithAuthor(author =>
                                {
                                    author
                                        .WithName("Punishment Log")
                                        .WithUrl("https://discordapp.com")
                                        .WithIconUrl("https://cdn.discordapp.com/embed/avatars/0.png");
                                })
                                .AddField("Reason:", "Use of Prohibited Word || " + x)
                                .AddField("Channel:", message.Channel)
                                .AddField("Message", stringtocheck)
                                .AddField("Punished By", "Automated Filter")
                                .WithColor(new Color(255, 0, 0));
                            var embed = builder.Build();
                            await channel.SendMessageAsync(embed: embed);
                        break;
                        }

                }
            }

        }
    }
}
