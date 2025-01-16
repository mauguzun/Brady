using Brady.Domain.Models.Generators;

namespace Brady.Infrastructure.Interfaces
{
    public interface IGenerationReportRepository
    {
        public GenerationReport LoadXml(string filename);
        public void DeleteXml(string filename);
    }
}
