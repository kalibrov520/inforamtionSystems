Feature: File Management: List of inbound configurations

    As an Operator I want to be able to view a list of incoming configurations via DS portal
    Background: At least one Inbound Data Source exists
    Given At least one 'Inbound Data Source' exists in the system
    And 'Operator' lands on the 'Dashboard' page
    And 'Operator' clicks on the 'Inbound' tab in top menu

    @DSDS-42
    Scenario: Test user can see a list of Inbound configurations
        When 'Operator' observes the list of 'Inbound Data Source'
        Then 'Operator' can see a list of existing 'Inbound Data Source' items

    @DSDS-42
    Scenario: Test user can see a status of the last process run in Inbound configurations
        Given There is at least one process run for selected 'Inbound Data Source'
        When 'Operator' observes selected 'Inbound Data Source'
        Then 'Operator' can see 'Last Running Date and Time' in 'Last Running' column
    
    @DSDS-42
    Scenario Outline: Test user can filter feeds by status
        Given There is "number" of 'Inbound Data Source' items in "status"
        When 'Operator' clicks on a "color" 'Filter' icon 
        Then 'Operator' can see a "count" 'Inbound Data Source' items with selected 'Feed Status' 
    Examples:
    | number | status | color | count|
    | 5 | Failed  | Red  | 5 |
    | 1 | Success  | Green  | 1 |
    | 2 | Late  | Yellow  | 2 |
    | 3 | Inactive  | Grey  | 3 |
    | 11 | All  | Blue  | 11 |

    @DSDS-42
    Scenario Outline: Test user can see a status dashboard
        Given There is "number" of 'Inbound Data Source' items in "status"  
        When 'Operator' observes the 'Chart' circles 
        Then 'Operator' observes a "circle color" 'Chart' circle
        And 'Operator' can see a "count" of 'Inbound Data Source' items in 'Chart'

    Examples:
    | number | status | circle color | count |
    | 5 | Failed  | Red  | 5 |
    | 1 | Success  | Green  | 1 |
    | 2 | Late  | Yellow  | 2 |
    | 3 | Inactive  | Grey  | 3 |
    | 11 | All  | Blue  | 11 |