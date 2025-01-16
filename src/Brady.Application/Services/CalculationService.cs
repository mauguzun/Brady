using Brady.Application.Interfaces;
using Brady.Domain.Enum;
using Brady.Domain.Models.Generators;
using Brady.Domain.Models.Generators.Types;
using Brady.Domain.Models.Output;
using Brady.Domain.Models.ReferenceData;
using Brady.Domain.Options;
using Brady.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;

namespace Brady.Application.Services
{
    public class CalculationService(IReferenceDataRepository factorRepository, IOptions<ApplicationOptions> options) : ICalculationService
    {

        private readonly ReferenceData referenceData = factorRepository.LoadXml(options.Value.ReferenceData) ?? throw new InvalidDataException();

        public List<CoalGenerator> ActualHeatRates(GenerationReport report)
        {
            return report.Coal
             .Select(g => new CoalGenerator
             {
                 Name = g.Name, 
                 HeatRate = g.TotalHeatInput / g.ActualNetGeneration
             }).ToList();
        }

        public List<Domain.Models.Output.Day> MaxEmissionGenerators(GenerationReport report)
        {
            return report.Gas
             .SelectMany(g => g.Generation.Select(d => new Domain.Models.Output.Day
             {
                 Name = g.Name,
                 Date = d.Date,
                 Emission = d.Energy * g.EmissionsRating * referenceData.Factors.EmissionsFactor.Medium
             }))
             .Concat(report.Coal
                 .SelectMany(g => g.Generation.Select(d => new Domain.Models.Output.Day
                 {
                     Name = g.Name,
                     Date = d.Date,
                     Emission = d.Energy * g.EmissionsRating * referenceData.Factors.EmissionsFactor.High
                 })))
             .GroupBy(m => m.Date)
             .Select(g => g.OrderByDescending(m => m.Emission).First())
             .OrderBy(m => m.Name)
             .ToList();
        }

        public List<Generator> Totals(GenerationReport report)
        {
            var generators = new List<Generator>();

            generators.AddRange(GetGenerators(report.Wind.Where(w => w.Location.ToLower() == WindType.Offshore.ToString().ToLower()), ValueFactor(GenerationType.Wind, WindType.Offshore)));
            generators.AddRange(GetGenerators(report.Wind.Where(w => w.Location.ToLower() == WindType.Onshore.ToString().ToLower()), ValueFactor(GenerationType.Wind, WindType.Onshore)));
            generators.AddRange(GetGenerators(report.Gas, ValueFactor(GenerationType.Gas)));
            generators.AddRange(GetGenerators(report.Coal, ValueFactor(GenerationType.Coal)));

            return generators.OrderBy(m=>m.Name).ToList();
        }


        #region calcualte logic
        private List<Generator> GetGenerators<T>(IEnumerable<T> source, decimal factor) where T : BaseGenerator =>
             source.Select(g => new Generator
             {
                 Name = g.Name,
                 Total = g.Generation.Sum(d => d.Energy * d.Price * factor)
             }).ToList();

        private decimal ValueFactor(GenerationType type, WindType windType = default)
            => type switch
            {
                GenerationType.Wind => windType switch
                {
                    WindType.Offshore => referenceData.Factors.ValueFactor.Low,
                    WindType.Onshore => referenceData.Factors.ValueFactor.High,
                    _ => throw new ArgumentException($"Unknown wind type: {windType}")
                },
                GenerationType.Gas or GenerationType.Coal => referenceData.Factors.ValueFactor.Medium,
                _ => throw new ArgumentException($"Unknown generation type: {type}")
            };
        #endregion

    }
}
