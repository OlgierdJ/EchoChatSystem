using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.ManagementCore;

public class ServerProfileConfiguration : IEntityTypeConfiguration<ServerProfile>
{
    public void Configure(EntityTypeBuilder<ServerProfile> builder)
    {
        builder.HasKey(b => new { b.AccountId, b.ServerId });
        builder.Property(b => b.KickFromServerOnVoiceLeave).IsRequired();
        builder.Property(b => b.Nickname).IsRequired();
        builder.Property(b => b.TimeJoined).HasDefaultValueSql("getdate()").IsRequired();//tænker obejct bliver lave nå en user joiner en server 
        builder.Property(b => b.JoinMethod).HasDefaultValue("Unknown").IsRequired();//tænker hvis vi ikke sender hvordan bliver det bare en unknown

        builder.HasOne(b => b.Account).WithMany(b => b.Servers).HasForeignKey(b => b.AccountId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.Server).WithMany(b => b.Members).HasForeignKey(b => b.ServerId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(b => b.Folder).WithMany(b => b.Servers).HasForeignKey(b => new { b.FolderId, b.FolderName }).OnDelete(DeleteBehavior.Restrict).IsRequired(false);

        builder.HasMany(b => b.Roles).WithOne(b => b.Profile).HasForeignKey(b => new { b.AccountId, b.ServerId }).OnDelete(DeleteBehavior.ClientCascade).IsRequired(false);
        builder.HasMany(b => b.CategoryMemberSettings).WithOne(b => b.Profile).HasForeignKey(b => new { b.AccountId, b.ServerId }).OnDelete(DeleteBehavior.ClientCascade).IsRequired(false);
        builder.HasMany(b => b.CategoryMemberPermissions).WithOne(b => b.Profile).HasForeignKey(b => new { b.AccountId, b.ServerId }).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
        builder.HasMany(b => b.TextChannelMemberSettings).WithOne(b => b.Profile).HasForeignKey(b => new { b.AccountId, b.ServerId }).OnDelete(DeleteBehavior.ClientCascade).IsRequired(false);
        builder.HasMany(b => b.TextChannelMemberPermissions).WithOne(b => b.Profile).HasForeignKey(b => new { b.AccountId, b.ServerId }).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
        builder.HasMany(b => b.VoiceChannelMemberSettings).WithOne(b => b.Profile).HasForeignKey(b => new { b.AccountId, b.ServerId }).OnDelete(DeleteBehavior.ClientCascade).IsRequired(false);
        builder.HasMany(b => b.VoiceChannelMemberPermissions).WithOne(b => b.Profile).HasForeignKey(b => new { b.AccountId, b.ServerId }).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
    }
}
