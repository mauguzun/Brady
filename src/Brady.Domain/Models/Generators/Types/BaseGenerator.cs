namespace Brady.Domain.Models.Generators.Types
{
    public class BaseGenerator
    {
        public required string Name { get; init; }
        public required List<Day> Generation { get; init; }
    }
}
