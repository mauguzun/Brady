using Brady.Domain.Models.Generators.Types.Coal;
using Brady.Domain.Models.Generators.Types.Gas;
using Brady.Domain.Models.Generators.Types.Wind;

namespace Brady.Domain.Models.Generators
{
    public sealed class GenerationReport
    {
        public required List<WindGenerator> Wind { get; init; }
        public required List<GasGenerator> Gas { get; init; }
        public required List<CoalGenerator> Coal { get; init; }
    }

   
}
