Feature: Submit Auction with Invalid Details

Scenario: User enters invalid details in the auction form
  Given I login and fill in the auction form inputs
  When I enter invalid details
  Then the auction will not be created