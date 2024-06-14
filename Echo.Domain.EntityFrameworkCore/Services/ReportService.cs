using Echo.Domain.Shared.Entities.EchoCore.ReportCore.Profile;
using Echo.Domain.EntityFrameworkCore.Services.Bases;
using Echo.Domain.Shared.Interfaces.Bases;

namespace Echo.Domain.EntityFrameworkCore.Services;

public class ReportService : BaseEntityService<ProfileReport, ulong> //, IReportService
{
    public ReportService(IRepository<ProfileReport> repository) : base(repository)
    {
    }
}