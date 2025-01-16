namespace Brady.Domain.Models.Output
{
    public sealed class GenerationOutput
    {
        public required List<Generator>  Totals { get; init; }
        public required List<Day> MaxEmissionGenerators { get; init; }
        public required List<CoalGenerator> ActualHeatRates { get; init; }
    }

}
