using Brady.Domain.Models.Output;

namespace Brady.Infrastructure.Interfaces
{
    public interface IGenerationOutputRepository
    {
        void CreateXml(GenerationOutput output, string filename);
    }
}
