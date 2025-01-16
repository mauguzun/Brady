using Brady.Domain.Models.Generators;
using Brady.Infrastructure.Interfaces;

namespace Brady.Infrastructure.Repositories
{
    public class GenerationReportRepository : BaseRepository<GenerationReport>, IGenerationReportRepository
    {
        public void DeleteXml(string filename) => File.Delete(filename);
    }
}
