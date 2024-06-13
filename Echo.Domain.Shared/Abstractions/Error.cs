namespace Echo.Domain.Shared.Abstractions;

public sealed record Error(string Code, string? Description = null)
{
    public static readonly Error None = new(string.Empty);

    public static implicit operator Result(Error error) => Result.Failure(error);
}

public class Result
{
    private Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);
}

public class Result<TValue>
{
    public Result()
    { }

    private TValue _value;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public TValue ValueOrDefault => _value;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public TValue Value
    {
        get
        {
            //ThrowIfFailed();

            return _value;
        }
        private set
        {
            //ThrowIfFailed();

            _value = value;
        }
    }

    /// <summary>
    /// Set value
    /// </summary>
    public Result<TValue> WithValue(TValue value)
    {
        Value = value;
        return this;
    }
}
