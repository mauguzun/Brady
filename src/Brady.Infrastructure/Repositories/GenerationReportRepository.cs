using Brady.Domain.Models.Generators;
using Brady.Infrastructure.Interfaces;
using System.Xml.Serialization;

namespace Brady.Infrastructure.Repositories
{
    public class GenerationReportRepository() : IGenerationReportRepository
    {

        public void DeleteXml(string filename) => File.Delete(filename);

        public GenerationReport LoadXml(string filename)
        {
            try
            {
                using var stream = File.OpenRead(filename);
                var serializer = new XmlSerializer(typeof(GenerationReport));

                return serializer.Deserialize(stream) as GenerationReport
                    ?? throw new InvalidCastException("Failed to deserialize XML correctly.");

            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"The file '{filename}' was not found.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Error while deserializing XML file: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error while processing the XML file: {ex.Message}", ex);
            }
        }
    }
}
