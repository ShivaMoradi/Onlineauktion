Feature: Logging in and logging out

Scenario: Website is launched and I log in and log out
Given I login using my credentials
Then I am logged in
Then I logout
