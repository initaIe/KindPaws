namespace KindPaws.SharedKernel.Others.ResultManagement;

public class Result
{
    protected Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; protected init; }
    public bool IsFailure => !IsSuccess;

    public static Result Success()
    {
        return new Result(true);
    }

    public static Result Failure()
    {
        return new Result(false);
    }

    public static implicit operator Result(bool isSuccess)
    {
        if (isSuccess)
            return Success();

        return Failure();
    }
}

public class Result<TError> : Result
{
    protected Result(bool isSuccess, TError error) : base(isSuccess)
    {
        if (isSuccess && error is not null)
            throw new InvalidOperationException("Result can not be success and contain an error.");

        if (!isSuccess && error is null)
            throw new InvalidOperationException("Result can not be failure and not contain an error.");

        Error = error;
        IsSuccess = isSuccess;
    }

    public TError Error { get; private init; }

    public new static Result<TError> Success()
    {
        return new Result<TError>(true, default!);
    }

    public static Result<TError> Failure(TError error)
    {
        return new Result<TError>(false, error);
    }

    public static implicit operator Result<TError>(bool isSuccess)
    {
        if (isSuccess)
            return Success();

        throw new InvalidOperationException("Cannot implicitly create a failure result without an error.");
    }

    public static implicit operator Result<TError>(TError error)
    {
        return Failure(error);
    }
}

public class Result<TValue, TError> : Result<TError>
{
    private readonly TValue _value;

    protected Result(bool isSuccess, TError error, TValue value) : base(isSuccess, error)
    {
        _value = value;
    }

    public TValue Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("Result can not be success and contain an error.");

    public static Result<TValue, TError> Success(TValue value)
    {
        return new Result<TValue, TError>(true, default!, value);
    }

    public new static Result<TValue, TError> Failure(TError error)
    {
        return new Result<TValue, TError>(false, error, default!);
    }

    public static implicit operator Result<TValue, TError>(TValue value)
    {
        return Success(value);
    }

    public static implicit operator Result<TValue, TError>(TError error)
    {
        return Failure(error);
    }
}