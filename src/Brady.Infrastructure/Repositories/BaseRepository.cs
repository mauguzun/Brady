using System.Xml.Serialization;

namespace Brady.Infrastructure.Repositories
{
    public class BaseRepository<T> where T : class
    {
        public T LoadXml(string fileName)
        {
            try
            {
                using var stream = File.OpenRead(fileName); 
                var serializer = new XmlSerializer(typeof(T));

                return (T)serializer.Deserialize(stream) ?? throw new InvalidCastException("Failed to deserialize XML correctly.");
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
