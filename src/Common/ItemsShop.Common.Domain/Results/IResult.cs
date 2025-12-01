namespace ItemsShop.Common.Domain.Results;

public interface IResult
{
    bool IsSuccess { get; set; }

    bool IsFailure { get; }

    string? Description { get; }

    ErrorType? Error { get; }
}
