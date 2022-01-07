using System;
using System.Collections.Generic;
using System.Text;

namespace Calendar.DB.Models
{
    public class ChannelConfig
    {
        public int ChannelConfigId { get; set; }
        public string ChannelId { get; set; }
        public string GuildId { get; set; }
        public int ChannelType { get; set; }
    }
}
