using Calendar.DB;
using Calendar.DB.Models;
using CalendarBot.Helpers;
using Discord;
using Discord.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Bot.Commands
{
    public class GetParticipantsCommand : ModuleBase<SocketCommandContext>
    {
        public CalendarContext _context { get; set; }

        [Command("get_signups"), Summary("Get all sign ups per event")]
        public async Task CreateEvent(string messageId)
        {
            EventMeeting eventMeeting = await GetEvent(messageId);

            if(eventMeeting == null)
            {
                await Context.Channel.SendMessageAsync("Event doesn't exist :eyes:");
                return;
            }

            EmbedHelper embedHelper = new EmbedHelper();
            Embed embed = embedHelper.BuildEmbedParticipants(eventMeeting);

            IUserMessage msg = await ReplyAsync(embed: embed);
        }

        private async Task<EventMeeting> GetEvent(string msgId)
        {
            return await _context.EventMeetings.Include(x => x.SignUps).FirstOrDefaultAsync(x => x.DiscordMessageId == msgId);
        }
    }
}
