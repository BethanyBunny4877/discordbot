using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace ConsoleApp3.Services
{
    public class ReactionHandlingModule
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

        public ReactionHandlingModule(IServiceProvider provider, DiscordSocketClient discord, CommandService commands)
        {
            _discord = discord;
            _commands = commands;
            _provider = provider;
            _discord.ReactionAdded += HandleReactionAddedAsync;
            _discord.ReactionRemoved += HandleReactionRemovedAsync;
        }
        public async Task InitializeAsync(IServiceProvider provider)
        {
            _provider = provider;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), services: null);
            // Add additional initialization code here...
        }
        public void HookReactionAdded(BaseSocketClient client)
    => client.ReactionAdded += HandleReactionAddedAsync;
        public async Task HandleReactionAddedAsync(Cacheable<IUserMessage, ulong> cachedMessage, ISocketMessageChannel originChannel, SocketReaction reaction)
        {
            var message = await cachedMessage.GetOrDownloadAsync();
            if (message != null && reaction.User.IsSpecified && reaction.User.Value.Username != _discord.CurrentUser.Username && message.Id == 672091460484464690)
            {
                if (reaction.Emote.Name == "Unreal")
                    await (reaction.User.Value as SocketGuildUser).AddRoleAsync((originChannel as SocketGuildChannel).Guild.GetRole(672081697289797642));
                else if (reaction.Emote.Name == "Unity")
                    await (reaction.User.Value as SocketGuildUser).AddRoleAsync((originChannel as SocketGuildChannel).Guild.GetRole(672081724624076810));
                else if (reaction.Emote.Name == "Blender")
                    await (reaction.User.Value as SocketGuildUser).AddRoleAsync((originChannel as SocketGuildChannel).Guild.GetRole(672081747944407061));
                else if (reaction.Emote.Name == "3DSMax")
                    await (reaction.User.Value as SocketGuildUser).AddRoleAsync((originChannel as SocketGuildChannel).Guild.GetRole(672081770392584202));
                
            }
        }
        public void HookReactionRemoved(BaseSocketClient client)
    => client.ReactionRemoved += HandleReactionRemovedAsync;
        public async Task HandleReactionRemovedAsync(Cacheable<IUserMessage, ulong> cachedMessage, ISocketMessageChannel originChannel, SocketReaction reaction)
        {
            var message = await cachedMessage.GetOrDownloadAsync();
            if (message != null && reaction.User.IsSpecified && reaction.User.Value.Username != _discord.CurrentUser.Username && message.Id == 672091460484464690)
            {
                if (reaction.Emote.Name == "Unreal")
                    await (reaction.User.Value as SocketGuildUser).RemoveRoleAsync((originChannel as SocketGuildChannel).Guild.GetRole(672081697289797642));
                else if (reaction.Emote.Name == "Unity")
                    await (reaction.User.Value as SocketGuildUser).RemoveRoleAsync((originChannel as SocketGuildChannel).Guild.GetRole(672081724624076810));
                else if (reaction.Emote.Name == "Blender")
                    await (reaction.User.Value as SocketGuildUser).RemoveRoleAsync((originChannel as SocketGuildChannel).Guild.GetRole(672081747944407061));
                else if (reaction.Emote.Name == "3DSMax")
                    await (reaction.User.Value as SocketGuildUser).RemoveRoleAsync((originChannel as SocketGuildChannel).Guild.GetRole(672081770392584202));

            }
        }

    }
}
