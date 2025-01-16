namespace Brady.Domain.Models.ReferenceData
{
    public sealed class Factors
    {
        public required Factor ValueFactor { get; init; }
        public required Factor EmissionsFactor { get; init; }

    }
}
