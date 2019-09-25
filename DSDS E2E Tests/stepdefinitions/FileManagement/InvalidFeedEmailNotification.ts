import { InboundPageObject } from "../../pages/inboundPage";
import { config } from "../../config/config";
import { browser, element, by, ExpectedConditions } from "protractor";
import { TestObject } from "protractor/built/driverProviders";
const { Given, When, Then, PendingException } = require("cucumber");
const chai = require("chai").use(require("chai-as-promised"));
const chaiHttp = require('chai-http');
chai.use(chaiHttp);
const expect = chai.expect;
const rp = require('request-promise');

const inboundPage: InboundPageObject = new InboundPageObject();



//File Management: Invalid feed email notification

//@DSDS-39 Test user gets a notification via email if file records have issues

Given(/^'Operator' is added to the 'Notification' list for selected 'Data Source'$/, async () => {
    throw new PendingException();
});

Given(/^A 'Feed File' contains issues$/, async () => {
    throw new PendingException();
});

When(/^A feed process starts$/, async () => {
    await expect(element.all(by.xpath("//mercer-table/table/tbody/tr")).count()).to.eventually.above(0);
});

Then(/^Operator can see an error email in his mailbox$/, async () => {
    throw new PendingException();
});

//@DSDS-39 Test user can get a notification via email if a feed file is missing

Given(/^A 'Feed File' is missing$/, async () => {
    throw new PendingException();
});

//@DSDS-39 Test user can get a notification via email if file is a duplicate
