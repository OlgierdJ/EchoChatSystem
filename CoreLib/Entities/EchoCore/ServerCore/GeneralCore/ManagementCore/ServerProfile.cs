using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using CoreLib.Entities.EchoCore.ServerCore.Management;

namespace CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore
{
    public class ServerProfile //: BaseEntity<ulong>
                               //Server variables for changing display.
    {
        public ulong AccountId { get; set; }
        public ulong ServerId { get; set; }

        public ulong? FolderId { get; set; }
        public string? FolderName { get; set; } 

        public bool KickFromServerOnVoiceLeave { get; set; } //set if invited through voiceinvite with guestlink enabled
        public string Nickname { get; set; } //overrides account name when entity is displayed to the client
        public DateTime TimeJoined { get; set; }
        public string JoinMethod { get; set; } //How the user joined the server <-- invitelink, guestlink, eventlink, etc
        public Account Account { get; set; }
        public Server Server { get; set; }
        public AccountServerFolder? Folder { get; set; }
        public ICollection<ServerProfileServerRole>? Roles { get; set; } //all serverroles granted to this serverprofile through ServerProfileServerRole
        public ICollection<ServerChannelCategoryMemberSettings>? CategoryMemberSettings { get; set; } //grouped permissions for this account in a specific channelcategory (used for managing the collection)
        public ICollection<ServerChannelCategoryMemberPermission>? CategoryMemberPermissions { get; set; } //specific permissions for this account in a specific channelcategory
        public ICollection<ServerTextChannelMemberSettings>? TextChannelMemberSettings { get; set; } //grouped permissions for this account in a specific textchannel (used for managing the collection)
        public ICollection<ServerTextChannelMemberPermission>? TextChannelMemberPermissions { get; set; } //specific permissions for this account in a specific textchannel
        public ICollection<ServerVoiceChannelMemberSettings>? VoiceChannelMemberSettings { get; set; } //grouped permissions for this account in a specific voicechannel (used for managing the collection)
        public ICollection<ServerVoiceChannelMemberPermission>? VoiceChannelMemberPermissions { get; set; } //specific permissions for this account in a specific voicechannel
    }
    public class ServerMembership
    //split profile and membership into two tables for clarification of responsability
    //this is essentially a participancy table which is linked to all tables of which should be cascaded if member leaves server
    {

    }
}
