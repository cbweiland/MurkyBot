# MurkyBot

Discord bot for creating and signing up for events through discord.  Set the channel for users to create events in, then set the channel for the events to be posted.  This allows admins to just use perms in discord to control who can post and who can see/sign up for events.  The posted events use the built in discord embeds.  To sign up for events, users just need to click the emotes attacked to the embed!


I threw this together in a day and then spent a little time after fixing/adding other supported events.  Does need some work refactoring to make the code cleaner.  It works but code could be better.

Currently set to use a Yaml file for the prefix for discord commands.  


**Stack**
- C#
- .NET CORE 3.1
- Discord.Net to interface with discord - https://github.com/discord-net/Discord.Net
- MySQL DB

**Future TODOs**
- Refactor code to be cleaner, including but not limited to...
- - Separate each event type out into their own class.  All inherit from a base class which includes the common functionality.
- - Remove Yaml file and move prefix + discord server token to appsettings.
- - Move event images to appsettings.
- - Better logging.
- - Clean up params for submitting events.  Not all events need the same params.
