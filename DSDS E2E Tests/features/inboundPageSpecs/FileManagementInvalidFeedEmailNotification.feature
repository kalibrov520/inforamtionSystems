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