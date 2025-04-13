using Microsoft.Playwright;

namespace UItests.Pages
{
    public class SignupPage
    {
        private readonly IPage _page;

        public IPage Page { get { return _page; } }

        // Locators
        private ILocator GetStarted => _page.Locator("//button[text()='Get Started']");
        private ILocator FirstnameField => _page.Locator("#user_first_name");
        private ILocator LastnameField => _page.Locator("#user_last_name");
        private ILocator EmailField => _page.Locator("#user_email");

        // Constructor
        public SignupPage(IPage page) => _page = page;

        // Actions
        public async Task NavigateToAsync(string url)
        {
            await _page.GotoAsync(url, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });
            Console.WriteLine($"Navigated to {url}");
        }

        public async Task<string> GetPageTitleAsync()
        {
            Console.WriteLine("Verifying page title");
            return await _page.TitleAsync();
        }


        public async Task FillSignupFormAsync(string username, string lastname, string email)
        {
            await GetStarted.ClickAsync();
            Console.WriteLine("Get Started button clicked");
            await FirstnameField.FillAsync(username);
            Console.WriteLine("First name field filled");
            await LastnameField.FillAsync(lastname);
            Console.WriteLine("Last name field filled");
            await EmailField.FillAsync(email);
            Console.WriteLine("Email field filled");
        }

        public async Task SubmitFormAsync()
        {
            Console.WriteLine("Looking for dynamic element and Submitting the form");
            var submitLocator = _page.Locator("button:has-text(\"Sign up\")");
            await submitLocator.WaitForAsync();
            await submitLocator.ClickAsync();
        }

        public async Task<string> GetSuccessMessageAsync()
        {
            Console.WriteLine("Looking for dynamic element and check the message");
            var locator = _page.Locator("div#alert_modal_message");
            await locator.WaitForAsync();
            return await locator.InnerTextAsync();
        }
    }
}
