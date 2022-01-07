using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarBot.ArguementTypes
{
    [NamedArgumentType]
    public class EventParam
    {
        public string EventType { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string CheckIn { get; set; }
        public string Description { get; set; }
    }
}
