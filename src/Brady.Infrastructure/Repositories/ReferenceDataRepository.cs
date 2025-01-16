using Brady.Domain.Models.Generators;
using Brady.Domain.Models.ReferenceData;
using Brady.Infrastructure.Interfaces;
using System.Xml.Serialization;

namespace Brady.Infrastructure.Repositories
{
    public class ReferenceDataRepository :BaseRepository<ReferenceData>, IReferenceDataRepository
    {
    }
}
