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
        Then Operator can see 'Process Run' log with following atributes: Status, Date, Success Rows, Failed Rows   