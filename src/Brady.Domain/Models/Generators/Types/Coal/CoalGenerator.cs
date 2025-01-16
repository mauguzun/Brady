namespace Brady.Domain.Models.Generators.Types.Coal
{
    public class CoalGenerator : BaseGenerator
    {

        public required decimal TotalHeatInput { get; init; }
        public required decimal ActualNetGeneration { get; init; }
        public required decimal EmissionsRating { get; init; }

    }
}
