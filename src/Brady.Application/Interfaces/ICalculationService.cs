using Brady.Domain.Models.Generators;
using Brady.Domain.Models.Output;

namespace Brady.Application.Interfaces
{
    public interface ICalculationService
    {
        List<CoalGenerator> ActualHeatRates(GenerationReport newReportData);
        List<Domain.Models.Output.Day> MaxEmissionGenerators(GenerationReport newReportData);
        public List<Generator> Totals(GenerationReport generationReport);
    }
}
