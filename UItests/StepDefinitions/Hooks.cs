using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace UItests.StepDefinitions
{
    [Binding]
    public class Hooks
    {
        private IPlaywright _playwright;
        private IBrowser _browser;

        [BeforeScenario]
        public async Task InitializePlaywright(ScenarioContext scenarioContext)
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var page = await _browser.NewPageAsync();

            // Register IPage in the DI container
            scenarioContext.ScenarioContainer.RegisterInstanceAs<IPage>(page);
        }

        [AfterScenario]
        public async Task CleanupPlaywright()
        {
            if (_browser != null)
            {
                await _browser.CloseAsync();
            }
            _playwright?.Dispose();
        }
    }
}
