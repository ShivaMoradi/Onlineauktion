Feature: Submit Auction

Scenario: User enters valid details in the auction form
  Given I login and fill in the auction form
  When I enter valid details
  Then the auction will be created
