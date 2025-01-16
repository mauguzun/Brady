using System.Xml.Serialization;

namespace Brady.Domain.Models.Generators
{ 
    public class Day
    {
        public required DateTime Date { get; init; }
        public required decimal Energy { get; init; }
        public required decimal Price { get; init; }
    }
}
