using System.Xml.Serialization;

namespace Brady.Domain.Models.Output
{

    [XmlRoot("Day")]
    public class Day
    {
        public required string Name { get; init; }
        public DateTime Date { get; init; }
        public decimal Emission { get; init; }
    }

}
