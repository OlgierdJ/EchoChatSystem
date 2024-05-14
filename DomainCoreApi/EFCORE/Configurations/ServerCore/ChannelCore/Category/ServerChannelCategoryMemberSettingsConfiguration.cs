using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.Category
{
    //need review
    public class ServerChannelCategoryMemberSettingsConfiguration : IEntityTypeConfiguration<ServerChannelCategoryMemberSettings>
    {
        /*public ulong ChannelCategoryId { get; set; } //where these settings belong
        public ulong ProfileId { get; set; } //profile of which these settings affect.


        public ServerChannelCategory ChannelCategory { get; set; }
        public ServerProfile Profile { get; set; }
        public ICollection<ServerChannelCategoryMemberPermission>? Permissions { get; set; }*/

        public void Configure(EntityTypeBuilder<ServerChannelCategoryMemberSettings> builder)
        {
            builder.HasKey(b => new {b.ProfileId,b.ChannelCategoryId});

            builder.HasMany(b=>b.Permissions).WithOne(b=>b.MemberSettings).HasForeignKey(b => new { b.ChannelCategoryId, b.ProfileId }).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.ChannelCategory).WithMany(b => b.MemberSettings).HasForeignKey(b => b.ChannelCategoryId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.Profile).WithMany(b => b.CategoryMemberSettings).HasForeignKey(b => b.ProfileId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}