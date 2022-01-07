using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Calendar.DB.Models
{
   public class SignUp
    {
        public int SignUpId { get; set; }
        public string PersonDiscordId { get; set; }
        public string EmoteClicked { get; set; }
        public DateTime DateTimeSignedUp { get; set; }

        [ForeignKey("EventMeeting")]
        public int EventMeetingId { get; set; }
        public EventMeeting EventMeeting { get; set; }

    }
}
