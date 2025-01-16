namespace Brady.Domain.Models.Output
{
    public sealed class Generator
    {
        public required string Name { get; init; }
        public decimal Total { get; set; }
    }

}
