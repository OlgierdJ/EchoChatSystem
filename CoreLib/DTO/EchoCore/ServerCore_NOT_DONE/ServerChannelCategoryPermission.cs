using CoreLib.Entities.Base;
using CoreLib.Entities.Enums;

namespace CoreLib.DTO.EchoCore.ServerCore
{
    public class ServerChannelCategoryPermission //: ServerChannelPermission 
        //duplicate of channelpermissions such that changes are reflected in both tables though data has to be manually changed aswell
    {
        public int ChannelCategoryMemberSettingsId { get; set; }
        public ServerChannelCategoryMemberSettings ChannelCategoryMemberSettings { get; set; }
    }
}