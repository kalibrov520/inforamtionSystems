Feature: File Management: Trigger feed process
        As an Operator I want to be able to manually trigger the file transmission process
        Background: At least one Inbound Data Source exists
        Given At least one 'Inbound Data Source' exists in the system
            And 'Operator' lands on the 'Dashboard' page
            And 'Operator' clicks on the 'Inbound' tab in top menu
    @DSDS-55
    Scenario: Test user can run file feed process by clicking a 'Run' button 
        When Operator hits 'Run' next to the data feed
        Then Operator can see a new 'Last Run' record on the Dashboard page for selected 'Data Source'
        And Operator can see a new historical record in 'Invalid Records' log