﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Text;

namespace UrlShortener.UnitTests.Abstractions;

/// <summary>
/// Taken from https://github.com/dotnet/aspnetcore/blob/main/src/Identity/test/Shared/MockHelpers.cs
/// to mock User and Role Manager
/// </summary>
public static class MockHelpers {

    public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class {
        var store = new Mock<IUserStore<TUser>>();
        var mgr = new Mock<UserManager<TUser>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);
        mgr.Object.UserValidators.Add(new UserValidator<TUser>());
        mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
        return mgr;
    }

    public static Mock<RoleManager<TRole>> MockRoleManager<TRole>(IRoleStore<TRole> store = null!) where TRole : class {
        store ??= new Mock<IRoleStore<TRole>>().Object;
        var roles = new List<IRoleValidator<TRole>> {
            new RoleValidator<TRole>()
        };
        return new Mock<RoleManager<TRole>>(store, roles, MockLookupNormalizer(), new IdentityErrorDescriber(), null!);
    }

    public static UserManager<TUser> TestUserManager<TUser>(IUserStore<TUser> store = null!) where TUser : class {
        store ??= new Mock<IUserStore<TUser>>().Object;
        var options = new Mock<IOptions<IdentityOptions>>();
        var idOptions = new IdentityOptions();
        idOptions.Lockout.AllowedForNewUsers = false;
        options.Setup(o => o.Value).Returns(idOptions);
        var userValidators = new List<IUserValidator<TUser>>();
        var validator = new Mock<IUserValidator<TUser>>();
        userValidators.Add(validator.Object);
        var pwdValidators = new List<PasswordValidator<TUser>> {
            new PasswordValidator<TUser>()
        };
        var userManager = new UserManager<TUser>(store, options.Object, new PasswordHasher<TUser>(),
            userValidators, pwdValidators, MockLookupNormalizer(),
            new IdentityErrorDescriber(), null!,
            new Mock<ILogger<UserManager<TUser>>>().Object);
        validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>())).Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
        return userManager;
    }

    public static RoleManager<TRole> TestRoleManager<TRole>(IRoleStore<TRole> store = null!) where TRole : class {
        store ??= new Mock<IRoleStore<TRole>>().Object;
        var roles = new List<IRoleValidator<TRole>> {
            new RoleValidator<TRole>()
        };
        return new RoleManager<TRole>(store, roles, MockLookupNormalizer(), new IdentityErrorDescriber(), null!);
    }

    public static ILookupNormalizer MockLookupNormalizer() {
        var normalizerFunc = new Func<string, string>(i => i == null ? null! : Convert.ToBase64String(Encoding.UTF8.GetBytes(i)).ToUpperInvariant());
        var lookupNormalizer = new Mock<ILookupNormalizer>();
        lookupNormalizer.Setup(i => i.NormalizeName(It.IsAny<string>())).Returns(normalizerFunc);
        lookupNormalizer.Setup(i => i.NormalizeEmail(It.IsAny<string>())).Returns(normalizerFunc);
        return lookupNormalizer.Object;
    }
}
