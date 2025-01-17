using Brady.Application.Interfaces;
using Brady.Domain.Enum;
using Brady.Domain.Models.ReferenceData;
using Brady.Domain.Options;
using Brady.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;

namespace Brady.Application.Services
{
    public class FactorService(IReferenceDataRepository factorRepository, IOptions<ApplicationOptions> options) : IFactorService
    {
        private readonly ReferenceData referenceData = factorRepository.LoadXml(options.Value.ReferenceData)!;
        public decimal EmissionFactor(GenerationType type) => type switch
        {
            GenerationType.Gas => referenceData.Factors.EmissionsFactor.Medium,
            GenerationType.Coal => referenceData.Factors.ValueFactor.Medium,
            _ => throw new ArgumentException($"Unknown generation type: {type}")
        };
      

        public decimal ValueFactor(GenerationType type, GenerationLocation windType = GenerationLocation.Offshore) => type switch
        {
            GenerationType.Wind => windType switch
            {
                GenerationLocation.Offshore => referenceData.Factors.ValueFactor.Low,
                GenerationLocation.Onshore => referenceData.Factors.ValueFactor.High,
                _ => throw new ArgumentException($"Unknown wind type: {windType}")
            },
            GenerationType.Gas or GenerationType.Coal => referenceData.Factors.ValueFactor.Medium,
            _ => throw new ArgumentException($"Unknown generation type: {type}")
        };
        

      
       
    }
}
