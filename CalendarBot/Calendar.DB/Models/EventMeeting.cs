using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Calendar.DB.Models
{
    public class EventMeeting
    {
        [Key]
        public int EventMeetingId { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string CheckIn { get; set; }
        public string Description { get; set; }
        public string EventType { get; set; }
        public string DiscordMessageId { get; set; }

        public ICollection<SignUp> SignUps { get; set; }

    }
}
