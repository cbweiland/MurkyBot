using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalendarBot.Commands
{
    public class BotStatusCommand : ModuleBase<SocketCommandContext>
    {
        [Command("botstatus"), Alias("HelloWorld"), Summary("Checks if bot is alive")]
        public async Task BotStatus()
        {
            await Context.Channel.SendMessageAsync("Bot is up and running");
        }
    }
}
