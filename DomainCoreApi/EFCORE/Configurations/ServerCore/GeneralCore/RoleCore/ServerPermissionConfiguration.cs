using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.RoleCore
{
    public class ServerPermissionConfiguration : IEntityTypeConfiguration<ServerPermission>
    //needs different types of permissions stolen straight from discord channel or server permissions such as:
    /*
     * ("Administrator", "Members with this permission will have every permission and will also bypass channel specific permissions or restrictions (for example, these members would get access to all private channels) **This is a dangerous permission to grant.")
     * ("Manage Events", "Allows members to edit and cancel events.")
     * ("Create Events", "Allows members to create events.")
     * ("Set Voice Channel Status", "Allows members to create and edit voice channel status.")
     * ("Move Members", "Allows members to disconnect or move other members between voice channels that the member with this permission has access to.")
     * ("Deafen Members", "Allows members to deafen other members, which means they wont be able to speak or hear others.") //server deafen
     * ("Mute Members", "Allows members to mute other members, which means they wont be able to speak.") //server mute
     * ("Priority Speaker", "") //lowers others volume
     * ("")
     * etc
     */
    {
        public void Configure(EntityTypeBuilder<ServerPermission> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Description).IsRequired(false);

            builder.HasOne(b => b.Category).WithMany(b => b.Permissions).HasForeignKey(b => b.CategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.RolePermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.CategoryMemberSettings).WithOne().HasForeignKey(b => b.ChannelCategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.CategoryMemberPermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.TextChannelMemberSettings).WithOne().HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.Restrict);//

            builder.HasMany(b => b.CategoryRolePermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.ServerVoiceChannelRolePermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.TextChannelRolePermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.TextChannelMemberPermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.VoiceChannelMemberSettings).WithOne().HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.VoiceChannelMemberPermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);

            builder.HasData(new ServerPermission
            {
                Id = 1,
                Name = "View Channels",
                Description = "Allows the role to see the public channels that are not private. It’s normal to grant this permission to almost all roles, but the channel/category permission settings usually void it."
            });
            builder.HasData(new ServerPermission
            {
                Id = 2,
                Name = "Manage Channels",
                Description = "Allows the role to access the channel settings of all channels it can see. Granting this permission can be extremely dangerous since deleted channels are not recoverable."
            });
            builder.HasData(new ServerPermission
            {
                Id = 3,
                Name = "Manage Roles",
                Description = "Allows the role to create, edit, and remove all the roles that are below itself in the hierarchy. Users with this role can also add and remove roles to/from members. Granting this permission can be extremely dangerous since deleted roles are not recoverable, and ill-intended users can grant dangerous permissions to others."
            });
            builder.HasData(new ServerPermission
            {
                Id = 4,
                Name = "Create Expressions",
                Description = "Allows the role to access the Emoji, Stickers, and Soundboard sections of the Server Settings. Users with this role can add expressions. Granting this permission can be dangerous since there isn’t an approval system for added expressions."
            });
            builder.HasData(new ServerPermission
            {
                Id = 5,
                Name = "Manage Expressions",
                Description = "Allows the role to access the Emoji, Stickers, and Soundboard sections of the Server Settings. Users with this role can edit and remove all expressions. Granting this permission can be dangerous since deleted expressions are not recoverable."
            });
            builder.HasData(new ServerPermission
            {
                Id = 6,
                Name = "View Audit Log",
                Description = "Allows the role to view the Audit Log section of the Server Settings. While the section doesn’t allow users to take any action, it can be dangerous to grant since there could be sensitive information in Audit Logs."
            });
            builder.HasData(new ServerPermission
            {
                Id = 7,
                Name = "View Server Insights",
                Description = "Allows the role to view the Server Insights section of Server Settings. While Server Insights contains a lot of important information, there’s no harm in sharing them unless you don’t want to."
            });
            builder.HasData(new ServerPermission
            {
                Id = 8,
                Name = "Manage Webhooks",
                Description = "Allows the role to add, edit, and remove webhooks. Granting this permission can be extremely dangerous since webhooks can bypass AutoMod, bots, and other moderation systems in place, allowing users to tag @everyone, post unwanted content, and similar ill-intended actions limitlessly and fast."
            });
            builder.HasData(new ServerPermission
            {
                Id = 9,
                Name = "Manage Server",
                Description = "Allows the role to adjust the server settings like name, icon, default settings, add bots, and change AutoMod rules. Granting this can be extremely dangerous since while server name, icon, and default settings can be easily fixed, removed AutoMod rules are not recoverable, and ill-intended bots can nuke (delete all the channels, ban all the members, etc.) your server."
            });
            builder.HasData(new ServerPermission
            {
                Id = 10,
                Name = "Create Invite",
                Description = "Allows the role to create custom invites for the server. It’s normal to grant this to all roles unless you want to have only certain invites available. Be sure to provide your invite links in the server for people to copy and paste if you don’t want to grant this permission."
            });
            builder.HasData(new ServerPermission
            {
                Id = 11,
                Name = "Change Nickname",
                Description = "Allows the users with permission to change their own nicknames on your server. It’s a normal permission to grant."
            });
            builder.HasData(new ServerPermission
            {
                Id = 12,
                Name = "Manage Nicknames",
                Description = "Allows the role to change the nicknames of other members. Granting this permission can be dangerous since ill-intended users might vandalize others’ profiles by changing their names."
            });
            builder.HasData(new ServerPermission
            {
                Id = 13,
                Name = "Kick Members",
                Description = "Allows the role to kick members that are below them in the user/role hierarchy by using the integrated /kick command or via the right-click menu. Granting this permission can be extremely dangerous since it allows the users to remove others from the server (kicked users can rejoin,) but it’s not the most dangerous part of it. Discord has a “pruning” feature - a feature that allows you to kick all the members that haven’t been in Discord in the last 7 or 30 days with no/selected roles. Pruning is a common vandalism method that can remove most users of a server. Preventing the prune vandalism is as simple as not granting the Kick Members permission."
            });
            builder.HasData(new ServerPermission
            {
                Id = 14,
                Name = "Ban Members",
                Description = "Allows the role to ban members that are below them in the user/role hierarchy by using the integrated /ban command or via the right-click menu. Granting this permission can be extremely dangerous since it allows the users to ban every single user that is below them in the hierarchy, and banned users cannot rejoin the server, even with other accounts, since all bans are IP bans. Bans can be manually removed via the Bans section of the Server Settings."
            });
            builder.HasData(new ServerPermission
            {
                Id = 15,
                Name = "Timeout Members",
                Description = "Allows the role to timeout other users via the right-click menu. Users who are timed out cannot send messages in any channel or speak in voice channels. Granting this permission can be dangerous since it allows users to prevent others from interacting with the community."
            });
            builder.HasData(new ServerPermission
            {
                Id = 16,
                Name = "Send Messages",
                Description = "Allows the role to send messages in channels they can see. It’s normal to grant this permission to almost all roles, but it is usually voided by the channel/category permission settings."
            });
            builder.HasData(new ServerPermission
            {
                Id = 17,
                Name = "Send Messages in Threads",
                Description = "Allows the role to send messages in threads they can see. It’s normal to grant this permission to almost all roles, but it is usually voided by the channel/category permission settings."
            });
            builder.HasData(new ServerPermission
            {
                Id = 18,
                Name = "Create Public Threads",
                Description = "Allows the role to create public threads in channels they can see. Although Discord has a limit of 1000 for active threads (no limit on inactive), allowing users to create threads can make moderation a bit harder."
            });
            builder.HasData(new ServerPermission
            {
                Id = 19,
                Name = "Create Private Threads",
                Description = "Allows the role to create private threads in channels they can see. The only way to see a private thread is to be mentioned in the thread or have the Manage Threads permission."
            });
            builder.HasData(new ServerPermission
            {
                Id = 20,
                Name = "Embed Links",
                Description = "Allows the role to display embedded content for the links they send. A common misconception about this permission is that it allows or disallows users to send links. There are a few ways to disallow users from sending links, but this permission is not it. It only manages the embedded content (marked red in the image below) of a link."
            });
            builder.HasData(new ServerPermission
            {
                Id = 21,
                Name = "Attach Files",
                Description = "Allows the role to attach files with any extension to the channels where they can send messages in. While this permission is normal to grant to every user in servers with a few thousand members, it can be mildly dangerous in situations where there are tens of thousands of members and a fast chat where moderation is also mildly difficult. Being able to attach files means they can literally attach any file, including malicious ones."
            });
            builder.HasData(new ServerPermission
            {
                Id = 22,
                Name = "Add Reactions",
                Description = "Allows the role to add reactions to messages they can see. When disallowed, users can still add reactions to the reactions that are already present."
            });
            builder.HasData(new ServerPermission
            {
                Id = 23,
                Name = "Use External Emoji",
                Description = "Allows the role to use emojis from other servers. It is usually granted to all users on most servers, just like the Use External Stickers permission. Don’t grant this permission if you want to ensure that no one uses ill-intended emojis on your server."
            });
            builder.HasData(new ServerPermission
            {
                Id = 24,
                Name = "Use External Stickers",
                Description = "Allows the role to use stickers from other servers. It is usually granted to all users on most servers, just like the Use External Emoji permission. Don’t grant this permission if you want to ensure that no one uses ill-intended stickers on your server."
            });
            builder.HasData(new ServerPermission
            {
                Id = 25,
                Name = "Mention @everyone, @here, and All Roles",
                Description = "Allows the role to mention @everyone, @here, and all the roles even if their “Allow anyone to @mention this role” option is turned off. Granting this permission can be extremely dangerous since it allows users to spam mention everyone in the server and makes way for Mention Raids (multiple users joining the server and spam mentioning multiple users or even everyone)."
            });
            builder.HasData(new ServerPermission
            {
                Id = 26,
                Name = "Manage Messages",
                Description = "Allows the role to delete and pin messages they can see. Granting this permission can be very dangerous since it allows users to delete multiple messages of other users, potentially deleting every single message in the server."
            });
            builder.HasData(new ServerPermission
            {
                Id = 27,
                Name = "Manage Threads",
                Description = "Allows the role to edit, close, and delete threads. Granting this permission can be very dangerous since it gives full control over threads, potentially deleting all of them."
            });
            builder.HasData(new ServerPermission
            {
                Id = 28,
                Name = "Read Message History",
                Description = "Allows the role to see every message sent in text channels. When disallowed, users only see messages when they’re online and in a text channel. It’s normal to grant this permission to everyone."
            });
            builder.HasData(new ServerPermission
            {
                Id = 29,
                Name = "Send Text-to-Speech Messages",
                Description = "Allows the user to use the /tts command, which triggers a text-to-speech player to read out the provided message to everyone who’s viewing the channel. Granting this permission can be mildly dangerous since a device reading an unwanted message out loud can be risky."
            });
            builder.HasData(new ServerPermission
            {
                Id = 30,
                Name = "Use Application Commands",
                Description = "Allows the permission to use application commands such as slash commands and right-click menu buttons. It’s normal to grant this permission to everyone since most commands and application functions are public-intended; users won’t be able to use a command that isn’t public (only available to staff)."
            });
            builder.HasData(new ServerPermission
            {
                Id = 31,
                Name = "Send Voice Messages",
                Description = "Allows the permission to send voice messages to the channels they can see using mobile devices. Discord introduced the voice message feature in April 2024. Granting this permission can be mildly dangerous since there’s currently no automatic moderation on voice messages, and ill-intended users can send unwanted voice messages."
            });
            builder.HasData(new ServerPermission
            {
                Id = 32,
                Name = "Connect",
                Description = "Allows the permission to join voice channels they can see. It’s normal to grant this permission to everyone. One common reason not to grant this permission is to block newcomers from joining voice channels, preventing a potential voice raid. The system most servers use in this case is once the user spends a certain amount of time, they’ll get a new role (via a bot or manually) that has Connect permission."
            });
            builder.HasData(new ServerPermission
            {
                Id = 33,
                Name = "Speak",
                Description = "Allows the permission to speak in voice channels. If a user doesn’t have this permission, they will be muted upon joining a voice channel. There are two ways they can talk: they get the Speak permission, or a user with Mute Members permission unmutes them. It’s normal to grant this permission to everyone."
            });
            builder.HasData(new ServerPermission
            {
                Id = 34,
                Name = "Video",
                Description = "Allows the role to turn on their camera and screen share in voice channels. While it’s normal to grant this permission to everyone, it can be mildly dangerous since there’s no automatic moderation system for video calls and screen sharing, allowing ill-intended users to display unwanted content."
            });
            builder.HasData(new ServerPermission
            {
                Id = 35,
                Name = "Use Activities",
                Description = "Allows the role to use the Activities feature. Activities are games and apps (like YouTube Watch Together, Blazing 8s, Gartic Phone, etc.) that are integrated into voice channels. It’s normal to grant this permission to everyone."
            });
            builder.HasData(new ServerPermission
            {
                Id = 36,
                Name = "Use Soundboard",
                Description = "Allows the role to use sounds from the Soundboard in voice channels. Granting this permission can be mildly dangerous since users can disturb other members by playing or spamming loud or unwanted sounds in voice channels."
            });
            builder.HasData(new ServerPermission
            {
                Id = 37,
                Name = "Use External Sounds",
                Description = "Allows the role to use soundboards of other servers in voice channels. Granting this permission can be mildly dangerous since other servers might have ill-intended sounds."
            });
            builder.HasData(new ServerPermission
            {
                Id = 38,
                Name = "Use Voice Activity",
                Description = "Allows the role to speak without Push-to-talk. Users who don’t have this permission will have to use push-to-talk to speak in voice channels."
            });
            builder.HasData(new ServerPermission
            {
                Id = 39,
                Name = "Priority Speaker",
                Description = "Allows the role to use the “Push to Talk (Priority)” keybind, which lowers the other users’ voice channel volume when pressed, thus allowing the user to be easily heard. While this permission isn’t risky to grant, usually only staff roles are granted."
            });
            builder.HasData(new ServerPermission
            {
                Id = 40,
                Name = "Mute Members",
                Description = "Allows the role to mute other users in voice channels so they won’t be able to speak. It’s a common misconception that this permission allows users to mute others in the sense that they won’t be able to send messages; this is not the case. Users need the Timeout Members permission to mute others (prevent them from sending messages.) Granting this permission can be dangerous since it allows users to prevent others from speaking in voice channels."
            });
            builder.HasData(new ServerPermission
            {
                Id = 41,
                Name = "Deafen Members",
                Description = "Allows the role to deafen other users in voice channels so they won’t be able to hear other users. Deafened users can still speak. Granting this permission can be dangerous since it allows users to prevent others from hearing others in voice channels."
            });
            builder.HasData(new ServerPermission
            {
                Id = 42,
                Name = "Move Members",
                Description = "Allows the role to move members between voice channels. The user with the permission can also join voice channels even if they’re at full capacity. They can also move members into voice channels that are at full capacity. Granting this permission can be dangerous since it allows users to move each other between voice channels, potentially disturbing conversations."
            });
            builder.HasData(new ServerPermission
            {
                Id = 43,
                Name = "Set Voice Channel Status",
                Description = "Allows the role to adjust voice channel status. Granting this permission is mildly dangerous since users can put unwanted content in the status."
            });
            builder.HasData(new ServerPermission
            {
                Id = 44,
                Name = "Request to Speak",
                Description = "Allows the role to request to speak in stage channels. Members who request to speak can be approved or denied by moderators. It’s normal to grant this permission to everyone."
            });
            builder.HasData(new ServerPermission
            {
                Id = 45,
                Name = "Create Events",
                Description = "Allows the role to create events. Granting this permission is dangerous since users can flood the server with all kinds of events."
            });
            builder.HasData(new ServerPermission
            {
                Id = 46,
                Name = "Manage Events",
                Description = "Allows the role to edit and delete all events. Granting this permission is dangerous since users with the role can disturb the server's events."
            });
            builder.HasData(new ServerPermission 
            { 
                Id = 47, 
                Name = "Administrator", 
                Description = "Members with this permission will have every permission and will also bypass channel specific permissions or restrictions (for example, these members would get access to all private channels) **This is a dangerous permission to grant."
            });
        }
    }
}