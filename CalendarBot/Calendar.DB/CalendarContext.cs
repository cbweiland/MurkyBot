using Calendar.DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calendar.DB
{
    public class CalendarContext : DbContext
    {
        public DbSet<EventMeeting> EventMeetings { get; set; }
        public DbSet<SignUp> SignUp { get; set; }
        public DbSet<ChannelConfig> ChannelConfigs { get; set; }

        public CalendarContext(DbContextOptions<CalendarContext> options)
            :base(options){}

    }
}
