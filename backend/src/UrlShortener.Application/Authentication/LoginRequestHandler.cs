using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UrlShortener.Domain.Base.Result;
using UrlShortener.Domain.DTOs.Output;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Authentication;

public record LoginRequest(string UserName, string Password) : IRequest<Result<TokenResponse>>;

internal class LoginRequestHandler(UserManager<User> _userManager, TimeProvider _timeProvider) : IRequestHandler<LoginRequest, Result<TokenResponse>> {
    public async Task<Result<TokenResponse>> Handle(LoginRequest request, CancellationToken cancellationToken) {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if(user is null || !await _userManager.CheckPasswordAsync(user, request.Password)) {
            return Result.Failure<TokenResponse>(Error.LoginFailed);
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName ?? "")
        };

        var expires = _timeProvider.GetUtcNow().UtcDateTime.AddHours(1);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TODO"));
        var token = new JwtSecurityToken(
            issuer: "TODO",
            audience: "TODO",
            claims: claims,
            expires: expires, //TODO
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return Result.Success(new TokenResponse(new JwtSecurityTokenHandler().WriteToken(token), (expires - _timeProvider.GetUtcNow().UtcDateTime).Seconds - 5);
    }
}
