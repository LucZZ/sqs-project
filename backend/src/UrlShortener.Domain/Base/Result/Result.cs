using System.Reflection;

namespace UrlShortener.Domain.Base.Result;
/// <summary>
/// Represents a result of some operation, with status information and possibly an status.
/// </summary>
public class Result {
    protected internal Result(bool isSuccess, Error error) {
        if (isSuccess && error != Error.None) {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None) {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Errors = [error];
    }

    protected internal Result(bool isSuccess, Error[] errors) {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error[] Errors { get; }

    public static Result Success() => new(true, Error.None);

    public static Result<TValue> Success<TValue>(TValue value) =>
        new(value, true, Error.None);

    public static Result Failure(Error error) =>
        new(false, error);

    public static Result Failure(Error[] errors) =>
        new(false, errors);

    public static Result<TValue> Failure<TValue>(Error error) =>
        new(default, false, error);

    public static Result<TValue> Failure<TValue>(Error[] errors) =>
        new(default, false, errors);

    public static Result<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);

    /*
     * Method for exceptionhandling pipline
     * called with reflection
     * Do not change!
     */
#pragma warning disable IDE0051
    private static Result<TValue> FailureExceptionHandling<TValue>(Error[] error) =>
        new(default, false, error);
#pragma warning restore IDE0051

    public static Result InstantiateFromGenericType(Type type, params Error[] error) {
        MethodInfo info = typeof(Result).GetMethod("FailureExceptionHandling", BindingFlags.Static | BindingFlags.NonPublic) ?? throw new Exception("Method not found!");

        MethodInfo m = info.MakeGenericMethod(type);

        Result result = m.Invoke(null, new[] { error }) as Result ?? throw new Exception("Method could not be invoked!");

        return result;
    }
}