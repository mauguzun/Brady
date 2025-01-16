using Brady.Domain.Models.ReferenceData;
using Brady.Infrastructure.Interfaces;
using System.Xml.Serialization;

namespace Brady.Infrastructure.Repositories
{
    public class ReferenceDataRepository : IReferenceDataRepository
    {
        public ReferenceData? LoadXml(string fileName)
        {
            try
            {
                using var stream = File.OpenRead(fileName);
                var serializer = new XmlSerializer(typeof(ReferenceData));

                return serializer.Deserialize(stream) as ReferenceData
                    ?? throw new InvalidCastException("Failed to deserialize XML correctly.");

            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"The file '{fileName}' was not found.", ex);
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
