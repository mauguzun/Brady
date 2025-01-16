namespace Brady.Domain.Models.Output
{
    public sealed class CoalGenerator
    {
        public required string Name { get; init; }
        public decimal HeatRate { get; init; }
    }
}
