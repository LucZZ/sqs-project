using Bogus;
using Microsoft.AspNetCore.Identity;
using UrlShortener.Domain.Entities;
using UrlShortener.Persistence.Database;

namespace UrlShortener.IntegrationTests.Abstractions;
public class TestDataSeeder {

    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly Faker _faker;

    public const string Password = "Abcdef_1";

    public readonly List<User> Users = [];

    public TestDataSeeder(ApplicationDbContext context, UserManager<User> userManager) {
        _context = context;
        _userManager = userManager;
        _faker = new Faker();
    }

    public async Task SeedUsersAsync() {
        var userFaker = new Faker<User>().CustomInstantiator(f => new User(f.Person.UserName));

        Users.AddRange(userFaker.Generate(3));

        foreach (var user in Users) {
            await _userManager.CreateAsync(user, Password);
        } 
    }
}
