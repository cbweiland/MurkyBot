using Calendar.Bot.Enums;
using Calendar.DB;
using Calendar.DB.Models;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Bot.Commands
{
    public class SetEventCreatingChannelCommand : ModuleBase<SocketCommandContext>
    {
        public CalendarContext _context { get; set; }

        [Command("set_eventCreateChannel"), Summary("Set channel where events will be posted")]
        public async Task SetPostingChannel(ulong channelId)
        {
            string guildId = base.Context.Guild.Id.ToString();

            ChannelConfig config = await _context.ChannelConfigs.FirstOrDefaultAsync(x => x.ChannelId == channelId.ToString()
                                                                                     && x.ChannelType == (int)ChannelConfigType.EventCreating
                                                                                     && x.GuildId == guildId);

            if (config != null)
            {
                IUserMessage msg = await ReplyAsync("Uh that channel is already set as the creating event channel");
                return;
            }
            else
            {
                ChannelConfig oldConfig = await _context.ChannelConfigs.FirstOrDefaultAsync(x => x.GuildId == guildId
                                                                          && x.ChannelType == (int)ChannelConfigType.EventCreating);

                if(oldConfig != null) 
                {
                    _context.ChannelConfigs.Remove(oldConfig);
                }

                ChannelConfig newConfig = new ChannelConfig();
                newConfig.ChannelId = channelId.ToString();
                newConfig.ChannelType = (int)ChannelConfigType.EventCreating;
                newConfig.GuildId = guildId;

                _context.Add(newConfig);
                await _context.SaveChangesAsync();

                IUserMessage msg = await ReplyAsync("Pretty sure it worked");
            }


        }
    }
}
