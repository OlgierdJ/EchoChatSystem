using CoreLib.Entities.EchoCore.ReportCore.Profile;
using CoreLib.Interfaces.Repositories;
using CoreLib.Repositories.Bases;
using DomainCoreApi.EFCORE;
using System.Linq.Expressions;

namespace DomainCoreApi.Repositories;

public class ReportRepository : BaseEntityRepository<ProfileReport>, IReportRepository
{
    public ReportRepository(EchoDbContext context) : base(context)
    {
    }

    public override Task<IEnumerable<ProfileReport>> GetAllWithIncludeAsync(Expression<Func<ProfileReport, bool>> expression = null)
    {
        throw new NotImplementedException();
    }

    public override Task<ProfileReport> GetSingleWithIncludeAsync(Expression<Func<ProfileReport, bool>> expression)
    {
        throw new NotImplementedException();
    }
}