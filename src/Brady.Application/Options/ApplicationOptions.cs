namespace Brady.Domain.Options
{
    public class ApplicationOptions
    {
        public required string InputFolder { get; init; }
        public required string OutputFolder { get; init; }
        public required string ReferenceData { get; init; }
        public required int Timeout { get; init; }

    }
}
