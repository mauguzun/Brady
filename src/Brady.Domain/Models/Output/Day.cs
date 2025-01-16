namespace Brady.Domain.Models.Output
{
    public sealed class Day
    {
        public required string Name { get; init; }
        public DateTime Date { get; init; }
        public decimal Emission { get; init; }
    }

}
