using Brady.Domain.Models.ReferenceData;

namespace Brady.Infrastructure.Interfaces
{
    public interface IReferenceDataRepository
    {
        public ReferenceData? LoadXml(string fileName);
    }
}
