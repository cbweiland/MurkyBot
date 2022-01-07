using Calendar.DB.Models;
using CalendarBot.ArguementTypes;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalendarBot.Helpers
{
    public class EmbedHelper
    {

        private readonly EmbedBuilder _embedbuilder;
        private List<IEmote> reactions;
        private const string HLLImage = "https://i.imgur.com/Q4Dsb1s.jpg";
        private const string ArmaImage = "https://i.imgur.com/urUfhfe.jpg";
        private const string NWImage = "https://i.imgur.com/INUrrtB.jpg";
        private const string DefaultImage = "https://i.imgur.com/R65N6l4.png";


        public EmbedHelper()
        {
            _embedbuilder = new EmbedBuilder();
            reactions = new List<IEmote>();
        }

        public Embed BuildEmbedNewEvent(EventParam eventParam)
        {
            _embedbuilder.Title = eventParam.Title;

            if (!string.IsNullOrWhiteSpace(eventParam.Date))
            {
                HandleDate(eventParam.Date);
            }

            if (!string.IsNullOrWhiteSpace(eventParam.Time))
            {
                HandleTime(eventParam.Time);
            }

            if (!string.IsNullOrWhiteSpace(eventParam.CheckIn))
            {
                HandleCheckIn(eventParam.CheckIn);
            }

            if (!string.IsNullOrWhiteSpace(eventParam.Description))
            {
                HandleDescription(eventParam.Description);
            }

            HandleEventType(eventParam.EventType);

            AddSignUpTip();

            return _embedbuilder.Build();
        }

        public Embed BuildEmbedParticipants(EventMeeting eventMeeting)
        {
            _embedbuilder.Title = eventMeeting.Title;

            _embedbuilder.AddField(name: "Participants",
                                    value: "-------------------------------",
                                    inline: false);

            StringBuilder participantsBuilder = new StringBuilder();

            string[] signUpOptions = GetEmoteOptions(eventMeeting.EventType);
                
            foreach (var option in signUpOptions)
            {
                IEnumerable<SignUp> signUpsForOption = eventMeeting.SignUps.Where(x => x.EmoteClicked == option).AsEnumerable();

                if (signUpsForOption.Count() == 0)
                {
                    participantsBuilder.Append("-");
                }
                else
                {
                    foreach (var participant in signUpsForOption)
                    {
                        participantsBuilder.AppendLine(participant.PersonDiscordId);
                    }
                }

                _embedbuilder.AddField(name: option,
                                        value: participantsBuilder.ToString(),
                                        inline: false);

                participantsBuilder.Clear();
            }



            return _embedbuilder.Build();
        }

        public IEmote[] GetReactions()
        {
            return reactions.ToArray();
        }

        public string[] GetEmoteOptions(string eventType)
        {
            switch (eventType.ToUpper())
            {
                case "50V50":
                    string[] hllString = { "🟥", "🟩", "🟨", "🟪", "🟦", "🔳" };
                    return hllString;
                case "18V18":
                    string[] hllSmallString = { "✅" };
                    return hllSmallString;
                case "ARMA":
                    string[] armaString = { "✅" };
                    return armaString;
                default:
                    string[] defaultString = { "✅" };
                    return defaultString;
            }
        }

        #region "Common Embeds"

        private void BuildCustomReaction(string emote)
        {
            var emoteOption = Emote.Parse(emote);
            reactions.Add(emoteOption);
        }

        private void BuildDefaultReaction(string emote)
        {
            var emoteOption = new Emoji(emote);
            reactions.Add(emoteOption);
        }

        private void HandleDate(string date)
        {
            _embedbuilder.AddField(name: "Date",
                        value: date,
                        inline: true);
        }

        private void HandleTime(string time)
        {
            _embedbuilder.AddField(name: "Time",
                                    value: time,
                                    inline: true);
        }

        private void HandleCheckIn(string checkIn)
        {
            _embedbuilder.AddField(name: "Check in",
                                    value: checkIn,
                                    inline: true);
        }

        private void HandleDescription(string description)
        {
            _embedbuilder.AddField(name: "Description",
                                    value: description,
                                    inline: false);
        }

        private void HandleEventType(string eventType)
        {
            switch (eventType.ToUpper())
            {
                case "50V50":
                    BuildHLL5050Event();
                    break;
                case "HLL":
                    BuildDefaultReaction("✅"); //Green Check
                    _embedbuilder.Color = Discord.Color.Red;
                    _embedbuilder.ThumbnailUrl = HLLImage;
                    break;
                case "ARMA":
                    BuildDefaultReaction("✅"); //Green Check
                    _embedbuilder.Color = Discord.Color.DarkGreen;
                    _embedbuilder.ThumbnailUrl = ArmaImage; //Arma Image
                    break;
                case "NW":
                    BuildDefaultReaction("✅"); //Green Check
                    _embedbuilder.Color = Discord.Color.Purple;
                    _embedbuilder.ThumbnailUrl = NWImage; //Arma Image
                    break;
                default:
                    BuildDefaultReaction("✅");
                    _embedbuilder.Color = Discord.Color.Blue;
                    _embedbuilder.ThumbnailUrl = DefaultImage; //The Camp Logo
                    break;
            }

        }

        private void AddSignUpTip()
        {
            _embedbuilder.AddField(name: "How to Sign up",
                                    value: "Just click the emote to sign up!  If you wish to take your name off just click the emote again.",
                                    inline: false);
        }

        #endregion


        #region "50v50"

        private void Build5050SignUpDescription(string[] options)
        {
            _embedbuilder.AddField(name: "Sign Up Options",
                                    value: "Pick **one** of the colors to sign up for",
                                    inline: false);

            _embedbuilder.AddField(name: "Red",
                                    value: options[0] + " Squads that sit in meat grinder pressing W + M1",
                                    inline: false);

            _embedbuilder.AddField(name: "Green",
                                    value: options[1] + " Flexible squads that move wherever needed on the map",
                                    inline: false);

            _embedbuilder.AddField(name: "Gold",
                                    value: options[2] + " Defensive squads",
                                    inline: false);

            _embedbuilder.AddField(name: "Purple",
                                    value: options[3] + " Arty + Recon",
                                    inline: false);

            _embedbuilder.AddField(name: "Blue",
                                    value: options[4] + " TANKS",
                                    inline: false);

            _embedbuilder.AddField(name: "Reserve",
                                    value: options[5] + " 'I can play if you need someone'",
                                    inline: false);
        }

        private void BuildHLL5050Event()
        {
            BuildDefaultReaction("🟥"); //Red Square
            BuildDefaultReaction("🟩"); //Green Square
            BuildDefaultReaction("🟨"); //Yellow Square
            BuildDefaultReaction("🟪"); //Purple Square
            BuildDefaultReaction("🟦"); //Blue Square
            BuildDefaultReaction("🔳"); //White Open Square

            string[] options = { "🟥", "🟩", "🟨", "🟪", "🟦", "🔳" };
            Build5050SignUpDescription(options);

            _embedbuilder.Color = Discord.Color.Red;
            _embedbuilder.ThumbnailUrl = HLLImage; //HLL Image
        }

        #endregion
    }
}
