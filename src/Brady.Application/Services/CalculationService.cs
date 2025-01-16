using Brady.Application.Interfaces;
using Brady.Domain.Enum;
using Brady.Domain.Models.Generators;
using Brady.Domain.Models.Generators.Types;
using Brady.Domain.Models.Output;

namespace Brady.Application.Services
{
    public class CalculationService(IFactorService factorService) : ICalculationService
    {
        private readonly IFactorService _factorService = factorService;
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
                 Emission = d.Energy * g.EmissionsRating * factorService.EmissionFactor(GenerationType.Gas)
             }))
             .Concat(report.Coal
                 .SelectMany(g => g.Generation.Select(d => new Domain.Models.Output.Day
                 {
                     Name = g.Name,
                     Date = d.Date,
                     Emission = d.Energy * g.EmissionsRating * factorService.EmissionFactor(GenerationType.Coal)   
                 })))
             .GroupBy(m => m.Date)
             .Select(g => g.OrderByDescending(m => m.Emission).First())
             .OrderBy(m => m.Name)
             .ToList();
        }

        public List<Generator> Totals(GenerationReport report)
        {
            var generators = new List<Generator>();

            generators.AddRange(GetGenerators(report.Wind.Where(w => w.Location.ToLower() == WindType.Offshore.ToString().ToLower()), factorService.ValueFactor(GenerationType.Wind, WindType.Offshore)));
            generators.AddRange(GetGenerators(report.Wind.Where(w => w.Location.ToLower() == WindType.Onshore.ToString().ToLower()), factorService.ValueFactor(GenerationType.Wind, WindType.Onshore)));
            generators.AddRange(GetGenerators(report.Gas, factorService.ValueFactor(GenerationType.Gas)));
            generators.AddRange(GetGenerators(report.Coal, factorService.ValueFactor(GenerationType.Coal)));

            return generators.OrderBy(m => m.Name).ToList();
        }


        #region calcualte logic
        private List<Generator> GetGenerators<T>(IEnumerable<T> source, decimal factor) where T : BaseGenerator =>
             source.Select(g => new Generator
             {
                 Name = g.Name,
                 Total = g.Generation.Sum(d => d.Energy * d.Price * factor)
             }).ToList();
        #endregion

    }
}
