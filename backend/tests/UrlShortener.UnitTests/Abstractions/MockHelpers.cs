using Microsoft.AspNetCore.Identity;
using Moq;

namespace UrlShortener.UnitTests.Abstractions;
public static class MockHelpers {
    public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser>? users = null)
        where TUser : class {
        var store = new Mock<IUserStore<TUser>>();

        var mgr = new Mock<UserManager<TUser>>(
            store.Object,
            null,  // IOptions<IdentityOptions>
            null,  // IPasswordHasher<TUser>
            new IUserValidator<TUser>[0],
            new IPasswordValidator<TUser>[0],
            null,  // ILookupNormalizer
            null,  // IdentityErrorDescriber
            null,  // IServiceProvider
            null   // ILogger<UserManager<TUser>>
        );

        if (users != null) {
            mgr.Setup(x => x.Users).Returns(users.AsQueryable());
        }

        return mgr;
    }
}
