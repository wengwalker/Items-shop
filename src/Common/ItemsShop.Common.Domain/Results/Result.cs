namespace ItemsShop.Common.Domain.Results;

public sealed class Result<T>
{
    public bool IsSuccess { get; set; }

    public bool IsFailure => !IsSuccess;

    public T? Value { get; }

    public string? Error { get; }

    public int? StatusCode { get; }

    private Result(bool isSuccess, T? value, string? error, int? statusCode)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        StatusCode = statusCode;
    }

    public static Result<T> Success()
    {
        return new Result<T>(true, default(T), null, null);
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, null, null);
    }

    public static Result<T> Failure(string error)
    {
        return new Result<T>(false, default(T), error, null);
    }

    public static Result<T> Failure(string error, int statusCode)
    {
        return new Result<T>(false, default(T), error, statusCode);
    }
}
