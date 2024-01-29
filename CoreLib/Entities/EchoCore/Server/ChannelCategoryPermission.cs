using CoreLib.Entities.Base;
using CoreLib.Entities.Enums;

namespace CoreLib.Entities.EchoCore.Server
{
    public class ChannelCategoryPermission : ChannelPermission 
        //duplicate of channelpermissions such that changes are reflected in both tables though data has to be manually changed aswell
    {
        public int ChannelCategoryMemberSettingsId { get; set; }
        public ChannelCategoryMemberSettings ChannelCategoryMemberSettings { get; set; }
    }
}