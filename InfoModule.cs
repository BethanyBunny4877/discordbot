using System;
using System.IO;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ConsoleApp3.Services;
using ConsoleApp3;
using System.Text.RegularExpressions;


namespace ConsoleApp3.Modules
{
    /*public class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("info")]   
        [Summary("Displays Bot Info")]
        
        public async Task Info()
        {
            
            Discord.IUser U = Context.Message.Author;

            string msg = "**Bot Info**\nTest";
            await Discord.UserExtensions.SendMessageAsync(U, msg);
            
            await Context.Message.DeleteAsync();
        }


    }
    public class AvatarModule : ModuleBase<SocketCommandContext>
    {
        [Command("avatar")]
        [Summary("Displays Bot Info")]

        public async Task Info()
        {

            Discord.IUser U = Context.Message.Author;
            string imgurl = U.AvatarId;
            string userurl = U.Id.ToString();
            string AvatartoEmbed = $"https://cdn.discordapp.com/avatars/{userurl}/{imgurl}.webp?size=1024";
            Random rnd = new Random();
            int red = rnd.Next(0, 255);
            int green = rnd.Next(0, 255);
            int blue = rnd.Next(0, 255);
            var EmbedAuth = new EmbedAuthorBuilder
            {
                Name = U.ToString(),
                
            };

            var EmbedFoot = new EmbedFooterBuilder
            {
                IconUrl = "https://cdn.discordapp.com/embed/avatars/0.png",
                Text = $"Generated - {DateTime.UtcNow.ToLongDateString()}",
            };

            var EmbedBuilder = new EmbedBuilder
            {
                Title = "Profile Picture",
                Author = EmbedAuth,
                ImageUrl = AvatartoEmbed,
                Footer = EmbedFoot,
                Url = AvatartoEmbed,
                Color = new Color(red, green, blue),

            
                
               
            }.Build();

            await Discord.UserExtensions.SendMessageAsync(U, embed: EmbedBuilder);


            await Context.Message.DeleteAsync();
        }


    }
    public class GetAvatarModule : ModuleBase<SocketCommandContext>
    {
        [Command("getavatar")]
        [Summary("Displays Bot Info")]

        public async Task Info(Discord.IUser user)
        {

            Discord.IUser U = user;
            Discord.IUser E = Context.Message.Author;
            string imgurl = U.AvatarId;
            string userurl = U.Id.ToString();
            string AvatartoEmbed = $"https://cdn.discordapp.com/avatars/{userurl}/{imgurl}.webp?size=1024";
            Random rnd = new Random();
            int red = rnd.Next(0, 255);
            int green = rnd.Next(0, 255);
            int blue = rnd.Next(0, 255);
            var EmbedAuth = new EmbedAuthorBuilder
            {
                Name = U.ToString(),

            };

            var EmbedFoot = new EmbedFooterBuilder
            {
                IconUrl = "https://cdn.discordapp.com/embed/avatars/0.png",
                Text = $"Generated - {DateTime.UtcNow.ToLongDateString()}",
            };

            var EmbedBuilder = new EmbedBuilder
            {
                Title = "Profile Picture",
                Author = EmbedAuth,
                ImageUrl = AvatartoEmbed,
                Footer = EmbedFoot,
                Url = AvatartoEmbed,
                Color = new Color(red, green, blue),




            }.Build();

            await Discord.UserExtensions.SendMessageAsync(E, embed: EmbedBuilder);


            await Context.Message.DeleteAsync();
        }


    }
    public class KickModule : ModuleBase<SocketCommandContext>
    {
        [Command("kick"), RequireUserPermission(GuildPermission.KickMembers)]
        [Summary("Displays Bot Info")]

        public async Task Info(IGuildUser user, string reason)
        {

            Discord.IUser E = Context.Message.Author;
            if (user.Username.ToString() == E.Username.ToString())
            {
                await Context.Channel.SendMessageAsync("You cannot use this command on yourself");
            }
            else if (user.Username.ToString() != E.Username.ToString())
            {
                var _client = Context.Client;
                string imgurl = user.AvatarId;
                string userurl = user.Id.ToString();
                string AvatartoEmbed = $"https://cdn.discordapp.com/avatars/{userurl}/{imgurl}.webp?size=1024";
                string PunishmentType = "Kick";
                ulong ID = 671665108593803264;
                var channel = _client.GetChannel(ID) as IMessageChannel;
                var EmbedAuth = new EmbedAuthorBuilder
                {
                    Name = user.ToString(),

                };

                var builder = new EmbedBuilder()
                    .WithTitle($"Punishment Type: {PunishmentType}")
                    .WithDescription($"Punished User: {user.Username.ToString()}")
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
                    .AddField("Reason:", reason)
                    .AddField("Punishment Length", "N/A - Kick")
                    .AddField("Punished By", E)
                    .WithColor(new Color(255, 0, 0));
                var embed = builder.Build();
                await channel.SendMessageAsync(embed: embed);
                await Discord.UserExtensions.SendMessageAsync(user, $"You were kicked from {Context.Guild.Name} for {reason}");
                await user.KickAsync(reason);
            } 
            else
            {
                await Context.Channel.SendMessageAsync("You dont have enough permissions to do that command");
            }
            await Context.Message.DeleteAsync();
        }


    }*/
    public class baradd : ModuleBase<SocketCommandContext>
    {
        [Command("filteradd"), RequireUserPermission(GuildPermission.KickMembers)]
        [Summary("Displays Bot Info")]

        public async Task Info(string WordtoAdd)
        {

            var con = new SqlConnection("Data Source=regigigas.database.windows.net;Initial Catalog=BadWords;Integrated Security=false;User ID=RetroGamerSP;Password=Herobrine4009");
            string[] blockedwords;
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
            string stringtocheck = WordtoAdd;
            foreach (string x in blockedwords)
            {
                if (stringtocheck.Contains(x))
                {
                    await Context.Message.Channel.SendMessageAsync($"The word `{stringtocheck}` is already in the filters database");
                    return;
                }
                else
                {
                    string addsql = @"INSERT INTO dbo.BarredWords (word)
               VALUES ('" + stringtocheck + "');";
                    using (var command = new SqlCommand(addsql, con))
                    {
                        await Context.Message.Channel.SendMessageAsync($"The word `{stringtocheck}` was added to the filters database");
                        command.ExecuteNonQuery();
                        return;
                    }
                }
            }
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
            await Context.Message.DeleteAsync();
        }


    }
    public class bardel : ModuleBase<SocketCommandContext>
    {
        [Command("filterdel"), RequireUserPermission(GuildPermission.KickMembers)]
        [Summary("Displays Bot Info")]

        public async Task Info(string Wordtodel)
        {

            var con = new SqlConnection("Data Source=regigigas.database.windows.net;Initial Catalog=BadWords;Integrated Security=false;User ID=RetroGamerSP;Password=Herobrine4009");
            string[] blockedwords;
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
            string stringtocheck = Wordtodel;
            foreach (string x in blockedwords)
            {
                int count = blockedwords.Length;
                int counter = 0;
                string test = x;
                if (stringtocheck == test)
                {
                    string addsql = @"DELETE FROM dbo.BarredWords
               WHERE word = '" + stringtocheck + "';";
                    using (var command = new SqlCommand(addsql, con))
                    {
                        await Context.Message.Channel.SendMessageAsync($"The word `{stringtocheck}` was removed from the filters database");
                        command.ExecuteNonQuery();
                        return;
                    }
                }
                else if (counter <= count && stringtocheck != test)
                {
                    counter = counter + 1;
                    if (counter == count)
                    {
                        await Context.Message.Channel.SendMessageAsync($"The word `{stringtocheck}` was not found in the filters database");
                    }
                }
                    
            }
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
            await Context.Message.DeleteAsync();
        }


    }
    public class RoomModule : ModuleBase<SocketCommandContext>
    {
        [Command("room")]
        [Summary("pokemon home room command")]

        public async Task Info(string action, string code = "null", [Remainder]string description = "A Generic Pokemon Home Trade Room")
        {
            string codepattern = @"^\d{12}$";
            Discord.IUser U = Context.Message.Author;
            if (action == "create")
            {
                if (Regex.IsMatch(code, codepattern))
                {
                    
                }
            }

            await Context.Message.DeleteAsync();
        }


    }

}
