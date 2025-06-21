using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UrlShortener.Domain.Base.Options;
using UrlShortener.Domain.Base.Result;
using UrlShortener.Domain.DTOs.Output;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Authentication;

public record LoginRequest(string UserName, string Password) : IRequest<Result<TokenResponse>>;

internal class LoginRequestHandler(UserManager<User> _userManager, TimeProvider _timeProvider, IOptions<JwtOptions> _jwtOptions) : IRequestHandler<LoginRequest, Result<TokenResponse>> {

    private const int BufferSeconds = 5;

    public async Task<Result<TokenResponse>> Handle(LoginRequest request, CancellationToken cancellationToken) {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if(user is null) {
            return Result.Failure<TokenResponse>(Error.LoginFailed);
        }
        if(!await _userManager.CheckPasswordAsync(user, request.Password)) {
            return Result.Failure<TokenResponse>(Error.LoginFailed);
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName ?? "")
        };

        var expires = _timeProvider.GetUtcNow().UtcDateTime.AddMinutes(_jwtOptions.Value.AccessTokenExpirationInMinutes);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.JWTSecret));
        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Value.ValidIssuer,
            audience: _jwtOptions.Value.ValidAudience,
            claims: claims,
            expires: expires,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return Result.Success(new TokenResponse(new JwtSecurityTokenHandler().WriteToken(token), (int)(expires - _timeProvider.GetUtcNow().UtcDateTime).TotalSeconds - BufferSeconds));
    }
}
