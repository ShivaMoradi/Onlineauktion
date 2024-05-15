Feature: Auction Page Button

    Scenario: Selecting an element from the selector and go back with auction back button
    Given I am visit the "auction" page
    When I select an element from the selector
    Then I should see the details of the selected item
    When I click on the "Back to Auction" button
    Then I should see all auctions displayed again