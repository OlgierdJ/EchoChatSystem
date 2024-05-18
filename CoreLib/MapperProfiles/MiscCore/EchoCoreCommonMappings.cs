using AutoMapper;
using CoreLib.DTO.EchoCore.MiscCore;
using CoreLib.DTO.EchoCore.MiscCore.ModerationCore;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.Entities.EchoCore;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ModerationCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;

namespace CoreLib.MapperProfiles.MiscCore
{
    public class EchoCoreCommonMappings : Profile
    {
        public EchoCoreCommonMappings() 
        {
            //CreateMap<Account, UserMinimalDTO>().ForMember(src => src.DisplayName, opts => opts.MapFrom(val => val.Profile.DisplayName)); //output members are same name so no need to map

            CreateMap<Country, CountryDTO>(); //output members are same name so no need to map
            CreateMap<Keybind, KeybindDTO>(); //output members are same name so no need to map
            CreateMap<AcceptedCurrency, CurrencyDTO>(); //output members are same name so no need to map
            CreateMap<Connection, ConnectionDTO>(); //output members are same name so no need to map
            //CreateMap<Account, BadgeDTO>(); //ignore at first cause complex business logic
            CreateMap<ServerRegion, RegionDTO>(); //output members are same name so no need to map
            //CreateMap<ServerAuditLog, AuditLogDTO>(); //output members are same name so no need to map
            CreateMap<ServerAuditLog, AuditLogDTO>().ForMember(src=>src.User, opts=>opts.MapFrom(val=>val.Account)); //output members are same name so no need to map
            CreateMap<AccountActivityStatus, ActivityStatusDTO>(); //output members are same name so no need to map
            CreateMap<AccountActivityStatus, ActivityStatusMinimalDTO>(); //output members are same name so no need to map
        }
       
    }
}