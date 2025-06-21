namespace UrlShortener.Domain.Base.Result;
/// <summary>
/// Represents the result of some operation, with status information and possibly a value and an error.
/// </summary>
public class Result<TValue> : Result {

    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error) =>
        _value = value;

    protected internal Result(TValue? value, bool isSuccess, Error[] errors)
        : base(isSuccess, errors) =>
        _value = value;

    public TValue? Value => IsSuccess
        ? _value!
        : default;
}
