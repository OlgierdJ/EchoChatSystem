using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Interfaces.Services
{
    public interface IAccountService : IEntityService<Account,ulong>
    {
        Task<Account> GetByUserId(ulong UserId);
    }
}
