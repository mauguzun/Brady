using Brady.Domain.Models.ReferenceData;
using Brady.Infrastructure.Interfaces;

namespace Brady.Infrastructure.Repositories
{
    public class ReferenceDataRepository :LoadRepository<ReferenceData>, IReferenceDataRepository
    {
    }
}
