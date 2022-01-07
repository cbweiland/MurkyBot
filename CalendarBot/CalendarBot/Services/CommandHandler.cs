using Calendar.Bot.Enums;
using Calendar.DB;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Bot.Services
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;
        private readonly IServiceProvider _provider;
        private readonly CalendarContext _context;

        // DiscordSocketClient, CommandService, IConfigurationRoot, and IServiceProvider are injected automatically from the IServiceProvider
        public CommandHandler(
            DiscordSocketClient discord,
            CommandService commands,
            IConfigurationRoot config,
            IServiceProvider provider,
            CalendarContext context)
        {
            _discord = discord;
            _commands = commands;
            _config = config;
            _provider = provider;
            _context = context;

            _discord.MessageReceived += OnMessageReceivedAsync;
        }

        private async Task OnMessageReceivedAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;     // Ensure the message is from a user/bot
            if (msg == null) return;
            if (msg.Author.Id == _discord.CurrentUser.Id) return;     // Ignore self when checking commands

            var Context = new SocketCommandContext(_discord, msg);     // Create the command context

            int argPos = 0;

            // Check if the message has a valid command prefix
            if (msg.HasStringPrefix(_config["prefix"], ref argPos) || msg.HasMentionPrefix(_discord.CurrentUser, ref argPos))
            {
                //When creating an event, should be in event creating channel
                if (msg.Content.Contains("create_event") && !await IsCreatingChannel(msg.Channel.Id))
                {
                    await Context.Channel.SendMessageAsync("Make sure you are in the Channel set for creating events");
                    return;
                }

                var result = await _commands.ExecuteAsync(Context, argPos, _provider);

                if (!result.IsSuccess)
                {
                    await Context.Channel.SendMessageAsync(result.ToString());
                }
            }
        }

        //Check if channel id exists in db as channel type event creating
        private async Task<bool> IsCreatingChannel(ulong channelId)
        {
            bool channelIsCreating = await _context.ChannelConfigs.AnyAsync(x => x.ChannelId == channelId.ToString()
                                                                && x.ChannelType == (int)ChannelConfigType.EventCreating);
            return channelIsCreating;
        }
    }
}
