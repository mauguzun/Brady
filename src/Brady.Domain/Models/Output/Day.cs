using System.Xml.Serialization;

namespace Brady.Domain.Models.Output
{
    public sealed class Day
    {
        public required string Name { get; init; }

        [XmlElement(nameof(Date))]
        public DateTime Date
        {
            get => _date;
            init => _date = DateTime.SpecifyKind(value, DateTimeKind.Unspecified);
        }

        public decimal Emission { get; init; }

        private DateTime _date;
       
    }
}
