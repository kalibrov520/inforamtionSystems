Feature: To test Inbound page functional

    @SpaceSearch
    Scenario: Space search on Inbound page
        Given I am on "inbound" page
            And I see N rows in the table
        When I click on "search" button
        Then I see same rows Count in the table




    @CleanButton
    Scenario: Clean text in search field
        Given I am on "inbound" page
        When I type "test"
            And I click on "clean" button
        Then I see clear search field

