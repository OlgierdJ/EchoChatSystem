using CoreLib.Entities.Base;

namespace CoreLib.DTO.EchoCore.ServerCore.Integrations
{
    public class ServerBotIntegrationCommandVoiceChannelOverride //: BaseEntity<ulong>
    {
        public ulong ServerBotIntegrationId { get; set; }
        public ulong ServerBotCommandId { get; set; }
        public ulong VoiceChannelId { get; set; }
        /// <summary>
        /// whether or not the bot is permitted to be used from a certain channel or not
        /// THE BOT IS REQUIRED TO HAVE READ AND WRITE PERMISSIONS FOR THE CHANNEL ON THEIR OWNED SERVER PROFILE IF THEY ARE PERMITTED IN THE CHANNEL.
        /// THIS OVERRIDES THE GLOBAL VOICE CHANNEL RESTRICTIONS
        /// </summary>
        public bool Permitted { get; set; }
        public ServerBotIntegration ServerBotIntegration { get; set; }
        public ServerBotCommand ServerBotCommand { get; set; }
       // public ServerVoiceChannel VoiceChannel { get; set; }
    }
}