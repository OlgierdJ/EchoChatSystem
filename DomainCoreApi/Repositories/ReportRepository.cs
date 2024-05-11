using CoreLib.Interfaces.Repositorys;
using CoreLib.Entities.EchoCore.ReportCore.Profile;
using CoreLib.Repositories.Bases;
using System.Linq.Expressions;
using DomainCoreApi.EFCORE;

namespace DomainCoreApi.Repositories
{
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
}