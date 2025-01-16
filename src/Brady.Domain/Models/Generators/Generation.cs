using System.Xml.Serialization;

namespace Brady.Domain.Models.Generators
{
    public class Generation
    {
        public required List<Day> Day { get; init; }
    }
}
