Feature: Filter search Action Page

  Scenario: User filters action by name 
    Given I am on the Action Page
    When I enter "Honda" into the auction page search box
    Then I should see only cars with the brand "Honda" in the results