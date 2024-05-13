Feature: Car Auction

  Background:
    Given I am logged

  Scenario: Place a bid in the auction
    Given I am on the "Auction page"
    When I click on the selector option
    And I enter a bid amount
    Then I should see the new bid amount displayed
