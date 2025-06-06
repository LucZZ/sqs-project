namespace UrlShortener.Domain.Base.Result;
/// <summary>
/// Represents an error.
/// 200 OK                      - Successful request; the server has successfully understood and processed the request.
/// 400 BadRequest              - Missing or malformed request; the server cannot understand the request due to invalid syntax.
/// 403 Forbidden               - Access denied; the server rejects the request because the client lacks necessary permissions.
/// 404 Not Found               - Resource not found; the server cannot locate the requested resource.
/// 409 Conflict                - Conflict with the current resource state; the request conflicts with the state of the resource on the server.
/// 500 Internal Server Error   - Internal server error; the server could not successfully process the request due to an unexpected error.
/// </summary>
/// 
public class Error : IEquatable<Error> {

    //General
    public static readonly Error None = new(string.Empty, string.Empty, 200);
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.", 404);

    //User
    public static readonly Error UserAlreadyExists = new("Error.UserAlreadyExists", "The user already exisit!", 409);
    public static readonly Error UserNotFound = new("Error.UserNotFound", "The user is not found!", 404);
    public static readonly Error RegistrationFailed = new("Error.RegistrationFailed", "The user registration failed!", 500);
    public static readonly Error LoginFailed = new("Error.LoginFailed", "Invalid Credentials!", 409);

    //Url
    public static readonly Error UrlAlreadyExists = new("Error.UrlAlreadyExists", "The url is already registered!", 409);
    public static readonly Error UrlBlocked = new("Error.UrlBlocked", "This url is blocked because fo security reasons!", 403);
    public static readonly Error UrlNotFound = new("Error.UrlNotFound", "This url was not found!", 404);

    public static readonly Error VirusTotalFailed = new("Error.VirusTotalFailed", "Virustotal api returned error", 400);
    public static readonly Error VirusTotalSuspicious = new("Error.VirusTotalSuspicious", "The URL has been flagged as unsafe by VirusTotal!", 400);



    public Error(string code, string message, int statusCode) {
        Code = code;
        Message = message;
        StatusCode = statusCode;
    }

    public string Code { get; }

    public string Message { get; }

    public int StatusCode { get; }

    public static implicit operator string(Error error) => error.Code;

    public static bool operator ==(Error? a, Error? b) {
        if (a is null && b is null) {
            return true;
        }

        if (a is null || b is null) {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Error? a, Error? b) => !(a == b);

    public virtual bool Equals(Error? other) {
        if (other is null) {
            return false;
        }

        return Code == other.Code && Message == other.Message && StatusCode == other.StatusCode;
    }

    public override bool Equals(object? obj) => obj is Error error && Equals(error);

    public override int GetHashCode() => HashCode.Combine(Code, Message);

    public override string ToString() => $"{Code}: {Message}";

    public static implicit operator Func<object, Error>(Error error) => value => error;
}
