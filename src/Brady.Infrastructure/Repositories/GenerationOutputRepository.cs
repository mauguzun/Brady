using Brady.Domain.Models.Output;
using Brady.Infrastructure.Interfaces;
using System.Xml.Serialization;

namespace Brady.Infrastructure.Repositories
{
    public class GenerationOutputRepository() : IGenerationOutputRepository
    {
       public void CreateXml(GenerationOutput output,string filename)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filename));

            var serializer = new XmlSerializer(typeof(GenerationOutput));
            using var stream = new FileStream(filename, FileMode.Create);
            serializer.Serialize(stream, output);
        }
    }

}
