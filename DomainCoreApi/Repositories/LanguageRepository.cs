using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Interfaces.Repositories;
using CoreLib.Repositories.Bases;
using DomainCoreApi.EFCORE;
using System.Linq.Expressions;

namespace DomainCoreApi.Repositories;

public class LanguageRepository : BaseEntityRepository<Language>, ILanguageRepository
{
    public LanguageRepository(EchoDbContext context) : base(context)
    {
    }

    public override Task<IEnumerable<Language>> GetAllWithIncludeAsync(Expression<Func<Language, bool>> expression = null)
    {
        throw new NotImplementedException();
    }

    public override Task<Language> GetSingleWithIncludeAsync(Expression<Func<Language, bool>> expression)
    {
        throw new NotImplementedException();
    }
}
