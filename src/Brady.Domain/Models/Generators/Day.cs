using System.Xml.Serialization;

namespace Brady.Domain.Models.Generators
{ 
    public sealed class Day
    {
        public required DateTimeOffset Date { get; init; }
        public required decimal Energy { get; init; }
        public required decimal Price { get; init; }
    }
}
