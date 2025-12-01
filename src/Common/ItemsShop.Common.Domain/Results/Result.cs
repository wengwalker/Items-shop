namespace ItemsShop.Common.Domain.Results;

public sealed class Result : IResult
{
    public bool IsSuccess { get; set; }

    public bool IsFailure => !IsSuccess;

    public string? Description { get; }

    public ErrorType? Error { get; }

    private Result(bool isSuccess, string? description, ErrorType? error)
    {
        IsSuccess = isSuccess;
        Description = description;
        Error = error;
    }

    public static Result Success()
    {
        return new Result(true, null, null);
    }

    public static Result Failure(string description, ErrorType error)
    {
        return new Result(false, description, error);
    }
}

public sealed class Result<T> : IResult
{
    public bool IsSuccess { get; set; }

    public bool IsFailure => !IsSuccess;

    public T? Value { get; }

    public string? Description { get; }

    public ErrorType? Error { get; }

    private Result(bool isSuccess, T? value, string? description, ErrorType? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Description = description;
        Error = error;
    }

    public static Result<T> Success()
    {
        return new Result<T>(true, default, null, null);
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, null, null);
    }

    public static Result<T> Failure(string description, ErrorType error)
    {
        return new Result<T>(false, default, description, error);
    }
}
