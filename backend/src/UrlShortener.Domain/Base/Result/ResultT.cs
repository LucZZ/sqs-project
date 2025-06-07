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

    public TValue? Value => IsSuccess
        ? _value!
        : default;

    public static implicit operator Result<TValue>(TValue? value) => Create(value);
}
