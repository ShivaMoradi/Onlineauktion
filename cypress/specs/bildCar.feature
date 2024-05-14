Feature: Car Auction

Background:
Given I am logged in as a user

Scenario: Verify valid bid
Given I am on the "home" page
When I click on a one car
When I enter a bid amount greater than the current highest bid
Then I should see a message confirming my bid was successful