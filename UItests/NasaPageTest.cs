using Microsoft.Playwright;
using UItests.Pages;

namespace UItests
{
    [TestFixture]
    public class UITests
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage? _page;

        [SetUp]
        public async Task Setup()
        {
            try
            {
                _playwright = await Playwright.CreateAsync();
                _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = false
                });
            } catch (PlaywrightException ex)
            {
                Assert.Fail($"Playwright setup failed: {ex.Message}");
            }
        }

        [Test]
        public async Task NavigateToPageAndFillRegistrationForm()
        {
            // Arrange environment
            _page = await _browser.NewPageAsync();
            var signupPage = new SignupPage(_page);
            await signupPage.NavigateToAsync("https://api.nasa.gov/");

            // Verify we are on correct page
            var pageTitle = await signupPage.GetPageTitleAsync();
            Assert.That(pageTitle, Is.EqualTo("NASA Open APIs"));

            // Act
            await signupPage.FillSignupFormAsync("Sviat", "Shunko", "testuser@example.com");
            await signupPage.SubmitFormAsync();

            // Assert message
            var successMessage = await signupPage.GetSuccessMessageAsync();
            Assert.That(successMessage, Does.Contain("Provide a valid email address"));
        }

        [TearDown]
        public async Task Teardown()
        {
            if (_browser != null)
            {
                await _browser.CloseAsync();
            }
            _playwright?.Dispose();
        }
    }
}