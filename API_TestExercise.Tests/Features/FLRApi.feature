
Feature: FLR API Response Validation

  Scenario: Valid request returns HTTP 200 with flare data
    Then the response should return HTTP 200 with flare data

  Scenario: Missing startDate returns HTTP 400
    Then the response should return HTTP 400 for missing startDate