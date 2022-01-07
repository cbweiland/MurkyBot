using Calendar.DB;
using Calendar.DB.Models;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Bot.Helpers
{
    public class ReactionHelper
    {
        public CalendarContext _context { get; set; }

        public ReactionHelper(CalendarContext context)
        {
            _context = context;
        }


        public async Task<int> AddReaction(string msgId, SocketReaction reaction)
        {
            //Get Event associated with Message ID
            EventMeeting eventMeeting = await GetEvent(msgId);

            //Save User in sign ups with that id
            int results = await SignUserUp(eventMeeting, reaction);

            return results;
        }

        public async Task<int> RemoveReaction(string msgId, SocketReaction reaction)
        {
            //Get Event associated with Message ID
            EventMeeting eventMeeting = await GetEvent(msgId);

            //Remove User in sign ups with that id
            int results = await RemoveUserSignUp(eventMeeting, reaction);

            return results;
        }

        private async Task<EventMeeting> GetEvent(string msgId)
        {
            EventMeeting eventMeeting = await _context.EventMeetings.FirstOrDefaultAsync(x => x.DiscordMessageId == msgId);

            if(eventMeeting == null)
            {
                throw new Exception("Event doesn't exist in the DB");
            }

            return eventMeeting;
        }

        private async Task<int> SignUserUp(EventMeeting eventMeeting, SocketReaction reaction)
        {
            SignUp signUp = new SignUp();
            signUp.DateTimeSignedUp = DateTime.Now;
            signUp.EmoteClicked = reaction.Emote.Name;
            signUp.EventMeetingId = eventMeeting.EventMeetingId;
            signUp.PersonDiscordId = reaction.User.ToString();

            _context.SignUp.Add(signUp);
            return await _context.SaveChangesAsync();
        }

        private async Task<int> RemoveUserSignUp(EventMeeting eventMeeting, SocketReaction reaction)
        {
            SignUp signUp = await _context.SignUp.FirstOrDefaultAsync(x => x.EventMeetingId == eventMeeting.EventMeetingId
                                                                && x.PersonDiscordId == reaction.User.ToString()
                                                                && x.EmoteClicked == reaction.Emote.Name);
            if(signUp != null)
            {
                _context.SignUp.Remove(signUp);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
