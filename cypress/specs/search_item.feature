Feature: Filter search functionality

  Scenario: User filters cars by brand
    Given I am on the homepage
    When I enter "Nissan" into the search box
    Then I should see only one  cars 