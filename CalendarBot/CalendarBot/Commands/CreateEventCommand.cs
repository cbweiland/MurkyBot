using CalendarBot.ArguementTypes;
using CalendarBot.Helpers;
using Calendar.DB;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Calendar.DB.Models;
using Microsoft.EntityFrameworkCore;
using Calendar.Bot.Enums;

namespace CalendarBot.Commands
{
    public class CreateEventCommand : ModuleBase<SocketCommandContext>
    {
        public CalendarContext _context { get; set; }

        [Command("create_event"), Summary("Create new event")]
        public async Task CreateEvent(EventParam eventParam)
        {
            string guildId = base.Context.Guild.Id.ToString();

            EmbedHelper embedHelper = new EmbedHelper();
            Embed embed = embedHelper.BuildEmbedNewEvent(eventParam);

            var channelConfig = await _context.ChannelConfigs.FirstOrDefaultAsync(x => x.ChannelType == (int)ChannelConfigType.EventPosting
                                                                                  && x.GuildId == guildId);
           
            if(channelConfig == null)
            {
                await Context.Channel.SendMessageAsync("Channel for posting events not set");
            }
            var channel = Context.Guild.GetTextChannel(Convert.ToUInt64(channelConfig.ChannelId));

            IUserMessage msg = await channel.SendMessageAsync(embed: embed);
            await SaveNewEvent(eventParam, msg);

            Thread.Sleep(1000);
            await msg.AddReactionsAsync(embedHelper.GetReactions());
        }

        private async Task<bool> SaveNewEvent(EventParam eventParam, IUserMessage msg)
        {
            EventMeeting newEvent = new EventMeeting();
            newEvent.Title = eventParam.Title;
            newEvent.Date = eventParam.Date;
            newEvent.Time = eventParam.Time;
            newEvent.CheckIn = eventParam.CheckIn;
            newEvent.Description = eventParam.Description;
            newEvent.EventType = eventParam.EventType;
            newEvent.DiscordMessageId = msg.Id.ToString();

            try
            {
                _context.EventMeetings.Add(newEvent);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync("Saving new event failed :(");
                return false;
            }
        }
    }
}

