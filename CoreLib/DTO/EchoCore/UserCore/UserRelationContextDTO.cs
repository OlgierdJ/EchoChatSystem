namespace CoreLib.DTO.EchoCore.UserCore
{
    public class UserRelationContextDTO
    {
        //public ulong ViewingUserId { get; set; } //get from jwt //further used on server upon making actions to see if they are allowed still.
        public ulong Id { get; set; } //user relations belong to

        public bool? CanCall { get; set; } //used to hide if self or grey if blocked //null = dont show //false = show but grey out // true = show and allow
        public bool? CanAlterPersonalNote { get; set; } //used to hide if self //null = dont show //false = show but grey out // true = show and allow
        public bool? CanAlterPersonalNickname { get; set; } //used to hide if self //null = dont show //false = show but grey out // true = show and allow
        public bool? CanMuteSoundboard { get; set; } //used to hide if self //null = dont show //false = show but grey out // true = show and allow
        public bool? CanDisableVideo { get; set; } //used to hide if self //null = dont show //false = show but grey out // true = show and allow
        public bool? CanAlterVolume { get; set; } //used to hide if self //null = dont show //false = show but grey out // true = show and allow
        public bool? CanAddRemoveFriend { get; set; } //used to hide if self or grey if blocked//null = dont show //false = show but grey out // true = show and allow
        public bool? CanBlock { get; set; } //used to hide if self //null = dont show //false = show but grey out // true = show and allow

        public bool VoiceMuted { get; set; } //always available
        public bool Deafened { get; set; } //always available

        public bool HasPersonalNote { get; set; }
        public bool HasPersonalNickname { get; set; }
        public bool SoundboardMuted { get; set; }
        public bool VideoDisabled { get; set; }
        public byte CurrentVolume { get; set; } //current volume
        public bool IsFriend { get; set; }
        public bool IsBlocked { get; set; }
    }

    public class ServerUserRelationContextDTO
    {
        public bool? CanServerMute { get; set; } //used to hide if no permission //null = dont show //false = show but grey out // true = show and allow 
        public bool? CanServerDeafen { get; set; } //used to hide if no permission //null = dont show //false = show but grey out // true = show and allow
        public bool? CanMoveTo { get; set; } //used to hide if no permission //null = dont show //false = show but grey out // true = show and allow
        public bool? CanTimeout { get; set; } //used to hide if self or no permission //null = dont show //false = show but grey out // true = show and allow
        public bool? CanKick { get; set; } //used to hide if self or no permission //null = dont show //false = show but grey out // true = show and allow
        public bool? CanBan { get; set; } //used to hide if self or no permission //null = dont show //false = show but grey out // true = show and allow
        public bool? CanServerNickname { get; set; } //alters view for self and other either displaying navigation or popup change nickname //null = dont show //false = show but grey out // true = show and allow
        public bool? CanServerDisconnect { get; set; } //used to hide if self or no permission //null = dont show //false = show but grey out // true = show and allow
        public bool? CanServerModView { get; set; } //used to hide if self or no permission //null = dont show //false = show but grey out // true = show and allow

        public bool VoiceServerMuted { get; set; }
        public bool VoiceServerDeafened { get; set; }
        public bool ServerSoundboardMuted { get; set; }
        public bool ServerVideoMuted { get; set; }
    }
}
