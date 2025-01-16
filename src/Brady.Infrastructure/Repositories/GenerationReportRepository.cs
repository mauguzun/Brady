using Brady.Domain.Models.Generators;
using Brady.Infrastructure.Interfaces;
using System.Xml.Serialization;

namespace Brady.Infrastructure.Repositories
{
    public class GenerationReportRepository : BaseRepository<GenerationReport>, IGenerationReportRepository
    {

        public void DeleteXml(string filename) => File.Delete(filename);

    }
}
