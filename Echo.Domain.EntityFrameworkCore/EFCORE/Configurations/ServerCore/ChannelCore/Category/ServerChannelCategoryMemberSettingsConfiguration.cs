using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.Category;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ServerCore.ChannelCore.Category;

//need review
public class ServerChannelCategoryMemberSettingsConfiguration : IEntityTypeConfiguration<ServerChannelCategoryMemberSettings>
{

    public void Configure(EntityTypeBuilder<ServerChannelCategoryMemberSettings> builder)
    {
        builder.HasKey(b => new { b.AccountId, b.ChannelCategoryId });

        builder.HasMany(b => b.Permissions).WithOne(b => b.MemberSettings).HasForeignKey(b => new { b.AccountId, b.ChannelCategoryId }).OnDelete(DeleteBehavior.ClientCascade);

        builder.HasOne(b => b.ChannelCategory).WithMany(b => b.MemberSettings).HasForeignKey(b => b.ChannelCategoryId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(b => b.Profile).WithMany(b => b.CategoryMemberSettings).HasForeignKey(b => new { b.AccountId, b.ServerId }).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(b => b.Server).WithMany(b => b.ChannelCategoryMemberSettings).HasForeignKey(b => b.ServerId).OnDelete(DeleteBehavior.NoAction);
    }
}