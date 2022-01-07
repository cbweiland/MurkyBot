using Calendar.Bot.Enums;
using Calendar.DB;
using Calendar.DB.Models;
using Discord;
using Discord.Commands;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Calendar.Bot.Commands
{
    public class SetEventPostingChannelCommand : ModuleBase<SocketCommandContext>
    {
        public CalendarContext _context { get; set; }

        [Command("set_eventPostChannel"), Summary("Set channel where events will be posted")]
        public async Task SetPostingChannel(ulong channelId)
        {
            string guildId = base.Context.Guild.Id.ToString();

            ChannelConfig config = await _context.ChannelConfigs.FirstOrDefaultAsync(x => x.ChannelId == channelId.ToString()
                                                                                     && x.ChannelType == (int)ChannelConfigType.EventPosting
                                                                                     && x.GuildId == guildId);

            if(config != null)
            {
                IUserMessage msg = await ReplyAsync("Uh that channel is already set as the posting event channel");
                return;
            }
            else
            {
                ChannelConfig oldConfig = await _context.ChannelConfigs.FirstOrDefaultAsync(x => x.GuildId == guildId
                                                                          && x.ChannelType == (int)ChannelConfigType.EventPosting);

                if (oldConfig != null)
                {
                    _context.ChannelConfigs.Remove(oldConfig);
                }

                ChannelConfig newConfig = new ChannelConfig();
                newConfig.ChannelId = channelId.ToString();
                newConfig.ChannelType = (int)ChannelConfigType.EventPosting;
                newConfig.GuildId = guildId;

                _context.Add(newConfig);
                await _context.SaveChangesAsync();

                IUserMessage msg = await ReplyAsync("Pretty sure it worked");
            }
        }
    }
}
