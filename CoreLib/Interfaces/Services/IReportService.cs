using CoreLib.Entities.EchoCore.ReportCore.Profile;
using CoreLib.Interfaces.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Interfaces.Services
{
    public interface IReportService : IEntityService<ProfileReport, ulong>
    {
    }
}