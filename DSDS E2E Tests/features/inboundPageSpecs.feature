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

      Given I am in "inbound" page
      When I type "test"

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
        Given There is <number> of 'Inbound Data Source' items in <status>
        When 'Operator' clicks on a <color> 'Filter' icon 
        Then 'Operator' can see a <count> 'Inbound Data Source' items with selected 'Feed Status' 
    Examples:
    | number | status | color | count|
    | 5 | Failed  | Red  | 5 |
    | 1 | Success  | Green  | 1 |
    | 2 | Late  | Yellow  | 2 |
    | 3 | Inactive  | Grey  | 3 |
    | 11 | All  | Blue  | 11 |

    @DSDS-42
    Scenario Outline: Test user can see a status dashboard
        Given There is <number> of 'Inbound Data Source' items in <status>  
        When 'Operator' observes the 'Chart' circles 
        Then 'Operator' observes a <circle color> 'Chart' circle
        And 'Operator' can see a <count> of 'Inbound Data Source' items in 'Chart'

    Examples:
    | number | status | circle color | count |
    | 5 | Failed  | Red  | 5 |
    | 1 | Success  | Green  | 1 |
    | 2 | Late  | Yellow  | 2 |
    | 3 | Inactive  | Grey  | 3 |
    | 11 | All  | Blue  | 11 |

    Feature: File Management: Invalid feed email notification
    As an Operator I want to get notifications if something goes wrong with the file load
    @DSDS-39
    Scenario: Test user gets a notification via email if file records have issues
        Given 'Operator' is added to the 'Notification' list for selected 'Data Source'
            And A 'Feed File' contains issues
        When A feed process starts
        Then Operator can see an error email in his mailbox

    @DSDS-39
    Scenario: Test user can get a notification via email if a feed file is missing
        Given 'Operator' is added to the 'Notification' list for selected 'Data Source'
            And A 'Feed File' is missing
        When A feed process starts
        Then Operator can see an error email in his mailbox

    @DSDS-39
    Scenario: Test user can get a notification via email if file is a duplicate
        Given 'Operator' is added to the 'Notification' list for selected 'Data Source'
        When A feed process starts
        Then Operator can see an error email in his mailbox           

    Feature: File Management: Process run history
    
       As an Operator I want to be able to review every run status and check invalid records in Inbound process via DS user interface

        Background: At least one Inbound Data Source exists
    Given At least one 'Inbound Data Source' exists in the system
    And 'Operator' lands on the 'Dashboard' page
    And 'Operator' clicks on the 'Inbound' tab in top menu   

    @DSDS-41
    Scenario: Test user can view the history of process runs for a specific Data Source
        Given A feed process starts
        When  Operator clicks on 'Inbound Data Source' item in the list
        Then Operator lands on  'Feed History' page
            And Operator can see 'Process Run' log with following atributes: Status, Date, Success Rows, Failed Rows

    @DSDS-41
    Scenario: Test user can view the invalid records for specific Process Run
        Given A feed process starts 
        And A process is completed with 'Failed' status
        When  Operator clicks on 'Inbound Data Source' item in the list
        And Operator clicks on   'Feed History' page
            And Operator can see 'Process Run' log with following atributes: Status, Date, Success Rows, Failed Rows        
           

    


