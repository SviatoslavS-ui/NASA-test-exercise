using TechTalk.SpecFlow;
using UItests.Pages;

namespace UItests.StepDefinitions
{
    [Binding]
    public class SignupPageSteps
    {
        private readonly SignupPage _signupPage;

        public SignupPageSteps(SignupPage signupPage) => _signupPage = signupPage;

        [Given(@"I navigate to the URL '(.*)'")]
        public async Task GivenINavigateToTheURL(string url)
        {
            await _signupPage.NavigateToAsync(url);
        }

        [Then(@"the page title should be '(.*)'")]
        public async Task ThenThePageTitleShouldBe(string expectedTitle)
        {
            var actualTitle = await _signupPage.GetPageTitleAsync();
            Assert.That(expectedTitle, Is.EqualTo(actualTitle));
        }

        [When(@"I fill the signup form with first name '(.*)', last name '(.*)', and email '(.*)'")]
        public async Task WhenIFillTheSignupFormWithFirstNameLastNameAndEmail(string firstName, string lastName, string email)
        {
            await _signupPage.FillSignupFormAsync(firstName, lastName, email);
        }

        [When(@"I submit the signup form")]
        public async Task WhenISubmitTheSignupForm()
        {
            await _signupPage.SubmitFormAsync();
        }

        [Then(@"the success message should be '(.*)'")]
        public async Task ThenTheSuccessMessageShouldBe(string expectedMessage)
        {
            var actualMessage = await _signupPage.GetSuccessMessageAsync();
            Assert.That(actualMessage, Does.Contain("Provide a valid email address"));
        }
    }
}
