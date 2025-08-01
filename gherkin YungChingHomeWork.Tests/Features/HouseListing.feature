Feature: House Listing Management
    As a real estate agent
    I want to manage house listings
    So that I can maintain property information

Scenario: Create a new house listing
    Given I have a new house listing with the following details:
        | Name     | Address      | Price   |
        | 豪華別墅 | 台北市信義區 | 5000000 |
    When I submit the house listing
    Then the house listing should be created successfully
    And the response should contain the house listing ID

Scenario: Get all house listings
    Given there are existing house listings in the system
    When I request all house listings
    Then I should receive a list of house listings

Scenario: Update an existing house listing
    Given there is an existing house listing with ID 1
    When I update the house listing with new details:
        | Name       | Address      | Price   |
        | 更新別墅   | 台北市大安區 | 6000000 |
    Then the house listing should be updated successfully

Scenario: Delete a house listing
    Given there is an existing house listing with ID 1
    When I delete the house listing
    Then the house listing should be deleted successfully