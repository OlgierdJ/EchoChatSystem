using CoreLib.Entities.EchoCore.ReportCore.Profile;
using CoreLib.Interfaces.Bases;
using CoreLib.Interfaces.Services;
using DomainCoreApi.Services.Bases;

namespace DomainCoreApi.Services
{
    public class ReportService : BaseEntityService<ProfileReport, ulong> //, IReportService
    {
        public ReportService(IRepository<ProfileReport> repository) : base(repository)
        {
        }
    }
}