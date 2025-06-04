namespace UrlShortener.Domain.Base.Result;
/// <summary>
/// Represents the result of some operation, with status information and possibly a value and an error.
/// </summary>
public class Result<TValue> : Result {

    private TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error) =>
        _value = value;

    protected internal Result(TValue? value, bool isSuccess, Error[] errors)
        : base(isSuccess, errors) =>
        _value = value;

    public Result<TValue> AdjustValue(Action<TValue> action) {
        if (_value is not null) {
            action(_value);
        }
        return this;
    }

    public Result<T> Map<T>() => IsSuccess ? new Result<T>(default, true, Error.None) : new Result<T>(default, false, this.Errors);

    public TValue? Value => IsSuccess
        ? _value!
        : default;

    public static implicit operator Result<TValue>(TValue? value) => Create(value);
}
