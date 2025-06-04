using MediatR;
using Microsoft.AspNetCore.Identity;
using UrlShortener.Domain.Base.Result;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Authentication;

public record RegisterRequest(string UserName, string Password) : IRequest<Result>;

internal class RegisterRequestHandler(UserManager<User> _userManager) : IRequestHandler<RegisterRequest, Result> {

    public async Task<Result> Handle(RegisterRequest request, CancellationToken cancellationToken) {

        var userExists = await _userManager.FindByNameAsync(request.UserName);

        if(userExists is not null) {
            return Result.Failure(Error.UserAlreadyExists);
        }

        var result = await _userManager.CreateAsync(new User() { UserName = request.UserName });

        if(!result.Succeeded) {
            return Result.Failure(Error.RegistrationFailed);
        }

        return Result.Success();
    }
}
