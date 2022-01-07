using Calendar.Bot;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace CalendarBot
{
    class Program
    {
        public static Task Main(string[] args)
            => Startup.RunAsync(args);
    }
}
