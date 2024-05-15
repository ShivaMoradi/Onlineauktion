Feature: Filter search functionality

  Scenario: User filters cars by brand
    Given Hope Page
    When I enter "Nissan" into the search box
    Then I should see only cars with the brand "Nissan" in the result