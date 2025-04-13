
Feature: NASA Signup Page Test
  As a user
  I want to navigate to the NASA signup page
  So that I can fill out the registration form and verify the success message

  Scenario: Navigate to NASA page and fill registration form
    Given I navigate to the URL 'https://api.nasa.gov/'
    Then the page title should be 'NASA Open APIs'
    When I fill the signup form with first name 'Sviat', last name 'Shunko', and email 'testuser@example.com'
    And I submit the signup form
    Then the success message should be 'Provide a valid email address'
