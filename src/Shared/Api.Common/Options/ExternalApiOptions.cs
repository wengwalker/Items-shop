namespace Api.Common.Options;

public class ExternalApiOptions : BaseOptions
{
    public required string CatalogBaseUrl { get; init; }

    public required int RetryAttempts { get; init; }

    public required int EventsCountToBreak { get; init; }

    public required int DurationOfBreak { get; init; }
}
