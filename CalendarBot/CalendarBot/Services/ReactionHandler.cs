using Calendar.Bot.Enums;
using Calendar.Bot.Helpers;
using Calendar.DB;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Bot.Services
{
    public class ReactionHandler
    {
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;
        private readonly IServiceProvider _provider;
        private readonly ReactionHelper _helper;
        private readonly CalendarContext _context;

        // DiscordSocketClient, CommandService, IConfigurationRoot, and IServiceProvider are injected automatically from the IServiceProvider
        public ReactionHandler(
            DiscordSocketClient discord,
            CommandService commands,
            IConfigurationRoot config,
            IServiceProvider provider,
            ReactionHelper helper,
            CalendarContext context)
        {
            _discord = discord;
            _commands = commands;
            _config = config;
            _provider = provider;
            _helper = helper;
            _context = context;

            _discord.ReactionAdded += OnReactionAdded_Event;
            _discord.ReactionRemoved += OnReactionRemoved_Event;
        }

        private async Task OnReactionAdded_Event(Cacheable<IUserMessage, UInt64> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if (reaction.UserId == _discord.CurrentUser.Id) return; //If bot we stop

            if (await IsPostingChannel(channel.Id))
            {
                await _helper.AddReaction(message.Id.ToString(), reaction);
            }
        }


        private async Task OnReactionRemoved_Event(Cacheable<IUserMessage, UInt64> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if (await IsPostingChannel(channel.Id))
            {
                await _helper.RemoveReaction(message.Id.ToString(), reaction);
            }
        }

        //Check if channel id exists in db as channel type event posting
        private async Task<bool> IsPostingChannel(ulong channelId)
        {
            var channelIsPosting = await _context.ChannelConfigs.AnyAsync(x => x.ChannelId == channelId.ToString()
                                                                && x.ChannelType == (int)ChannelConfigType.EventPosting);
            return channelIsPosting;
        }

    }
}
