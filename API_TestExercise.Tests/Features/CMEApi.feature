
Feature: CME API Response Validation

  Scenario: Valid request returns HTTP 200 with a non-empty list
    Then the response should return HTTP 200 with a non-empty list

  Scenario: Invalid date format returns HTTP 400.
    Then the response should return HTTP 400 with error message