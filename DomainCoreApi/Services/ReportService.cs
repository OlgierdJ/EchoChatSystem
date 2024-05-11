using CoreLib.Entities.EchoCore.ReportCore.Profile;
using CoreLib.Interfaces.Bases;
using CoreLib.Interfaces.Services;
using DomainCoreApi.Services.Bases;

namespace DomainCoreApi.Services
{
    //ved ikke lige hvorfor jeg skal tilføj interface
    public class ReportService : BaseEntityService<ProfileReport, ulong>, IReportService
    {
        public ReportService(IRepository<ProfileReport> repository) : base(repository)
        {
        }

        public Task<ReportedProfile> AddAsync(ReportedProfile entity)
        {
            throw new NotImplementedException();
        }

        public Task<ReportedProfile> UpdateAsync(ReportedProfile entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReportedProfile>> UpdateAsync(IEnumerable<ReportedProfile> entities)
        {
            throw new NotImplementedException();
        }

        Task<ReportedProfile> IEntityService<ReportedProfile, ulong>.DeleteAsync(ulong id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ReportedProfile>> IEntityService<ReportedProfile, ulong>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ReportedProfile>> IEntityService<ReportedProfile, ulong>.GetAllWithIncludeAsync()
        {
            throw new NotImplementedException();
        }

        Task<ReportedProfile> IEntityService<ReportedProfile, ulong>.GetSingleAsync(ulong id)
        {
            throw new NotImplementedException();
        }

        Task<ReportedProfile> IEntityService<ReportedProfile, ulong>.GetSingleWithIncludeAsync(ulong id)
        {
            throw new NotImplementedException();
        }
    }
}
//