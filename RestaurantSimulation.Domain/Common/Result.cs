namespace RestaurantSimulation.Domain.Common;

public readonly record struct Result<T> 
{
    public bool IsSuccess { get; }
    public Error? Error { get; }
    public T? Value { get; }

    private Result(bool isSuccess, T? value, Error? error)
    {
        if (isSuccess && error is not null)
        {
            throw new ArgumentException();
        }
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, null);
    }

    public static Result<T> Failure(Error error)
    {
        return new Result<T>(false, default,  error);
    }
}

public readonly record struct Result
{
    public bool IsSuccess { get; }
    public Error? Error { get; }

    private Result(bool isSuccess,  Error? error)
    {
        if (isSuccess && error is not null)
        {
            throw new ArgumentException();
        }
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success()
    {
        return new Result(true, null);
    }

    public static Result Failure(Error error)
    {
        return new Result(false,  error);
    }
}

public abstract record Error(string Message);