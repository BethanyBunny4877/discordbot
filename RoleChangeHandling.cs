using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace ConsoleApp3.Services
{
    /*public class RoleHandlingModule
    {
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private IServiceProvider _provider;
        public static ulong messageid;
        public static ulong ID
        {
            get {return messageid;}
            set {messageid = value;}
        }

        public RoleHandlingModule(IServiceProvider provider, DiscordSocketClient discord, CommandService commands)
        {
            _discord = discord;
            _commands = commands;
            _provider = provider;
            _discord.GuildMemberUpdated += HandleRoleChangeAsync;
        }
        public async Task InitializeAsync(IServiceProvider provider)
        {
            _provider = provider;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), services: null);
            // Add additional initialization code here...
        }
        public void HandleRoleChange(BaseSocketClient client)
    => client.GuildMemberUpdated += HandleRoleChangeAsync;
        public async Task HandleRoleChangeAsync(SocketGuildUser before, SocketGuildUser after)
        {
            if (before.Roles != after.Roles)
            {
                ulong ID = 671665108593803264;
                var channel = _discord.GetChannel(ID) as IMessageChannel;
                string[] rolesbefore;
                string[] rolesafter;
                foreach(SocketRole role in before.Roles)
                {
                    Console.WriteLine("test");
                }

                await channel.SendMessageAsync(before.Mention + "'s roles have been changed from " + before.Roles.ToString() + " to" + after.Roles.ToString());

            }
            

        }

    }*/
}
