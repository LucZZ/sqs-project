using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;

namespace UrlShortener.E2ETests;

public class BasicTests : PageTest {

    [Fact]
    public async Task RegisterTest() {
        await Page.GotoAsync("http://localhost:8080/register");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" }).FillAsync($"testuser123{Guid.NewGuid()}");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).FillAsync("Abcdefgth_1!");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();

        await Expect(Page).ToHaveURLAsync(new Regex(".*/login"));
    }

    [Fact]
    public async Task LoginTest() {
        var username = $"testuser123{Guid.NewGuid()}";
        var password = "Abcdefgth_1!";
        await Page.GotoAsync("http://localhost:8080/register");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" }).FillAsync(username);
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).FillAsync(password);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
        await Page.GotoAsync("http://localhost:8080/login");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" }).FillAsync(username);
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).FillAsync(password);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

        await Expect(Page).ToHaveURLAsync(new Regex(".*/dashboard"));
    }

    [Fact]
    public async Task DashboardTest() {
        await Page.GotoAsync("http://localhost:8080/dashboard");

        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Original URL" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Original URL" }).FillAsync("ada");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Shorten" }).ClickAsync();
        await Expect(Page.Locator("text=An unexpected error occurred.")).ToBeVisibleAsync();
    }

}
