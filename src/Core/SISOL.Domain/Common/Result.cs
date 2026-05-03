namespace SISOL.Domain.Common;

public class Result
{
    protected internal Result(bool isSuccess, string? error, IEnumerable<string>? errors = null)
    {
        if (isSuccess && (error != null || errors?.Any() == true))
            throw new InvalidOperationException();

        if (!isSuccess && error == null && errors?.Any() != true)
            throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Error = error;
        Errors = errors?.ToList() ?? [];
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? Error { get; }
    public IReadOnlyCollection<string> Errors { get; }

    public static Result Success() => new(true, null);
    public static Result Failure(string? error) => new(false, error);
    public static Result Failure(IEnumerable<string> errors) => new(false, null, errors);
    public static Result<T> Success<T>(T value) => new(value, true, null);
    public static Result<T> Failure<T>(string error) => new(default, false, error);
    public static Result<T> Failure<T>(IEnumerable<string> errors) => new(default, false, null, errors);
}

public class Result<T> : Result
{
    protected internal Result(T? value, bool isSuccess, string? error, IEnumerable<string>? errors = null)
    : base(isSuccess, error, errors)
    {
        Value = value;
    }

    public T? Value { get; }
}
