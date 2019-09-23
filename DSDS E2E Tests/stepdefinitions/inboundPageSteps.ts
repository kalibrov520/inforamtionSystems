import { InboundPageObject } from "../pages/inboundPage";
import { config } from "../config/config";
import { browser, element, by, ElementFinder, ElementArrayFinder } from "protractor";
import { TestObject } from "protractor/built/driverProviders";
const { Given, When, Then, PendingException } = require("cucumber");
const chai = require("chai").use(require("chai-as-promised"));
const chaiHttp = require('chai-http');
chai.use(chaiHttp);
const expect = chai.expect;
const rp = require('request-promise');

const inboundPage: InboundPageObject = new InboundPageObject();


Given(/^I am on "(.*?)" page$/, async (text) => {
    switch(text) {
        case "inbound":
            await browser.get(config.baseUrl);
            await expect(browser.getTitle()).to.eventually.equal("DS-PoC");
        break;
    }
});


Given(/^I see N rows in the table$/, async () => {
    InboundPageObject.rowCount = await element.all(by.xpath("//mercer-table/table/tbody/tr")).count();
});

When(/^I click on "(.*?)" button$/, async (text) => {

    switch(text) {
        case "search":
            await inboundPage.searchButton.click();
            break;
        case "clean":
            await inboundPage.cleanButton.click();
            break;
    }
});


When(/^I type "(.*?)"$/, async (text) => {
    await inboundPage.searchTextBox.sendKeys(text);
});


Then(/^I see clear search field$/, async () => {
    expect(undefined).to.equal(inboundPage.searchTextBox.value);
});


Then(/^I see same rows Count in the table$/, async () => {
    await expect(element.all(by.xpath("//mercer-table/table/tbody/tr")).count()).to.eventually.equal(InboundPageObject.rowCount);
});


//Background

Given(/^At least one 'Inbound Data Source' exists in the system$/, async () => { //?bugged


    let options = {
        uri: 'http://spb-mdspoc01.internal.corp:8080/engine-rest/process-definition',
        json: true 
    };
     
    await rp(options)
        .then(function (repos) {
            console.log('User has %d repos', repos.length);
            expect(repos.length).above(0);
        });


});

Given(/^'Operator' lands on the 'Dashboard' page$/, async () => {
    await browser.get(config.baseUrl);
});

Given(/^'Operator' clicks on the 'Inbound' tab in top menu$/, async () => {
    await inboundPage.inboundTab.click();
});

//@DSDS-42 Test user can see a list of Inbound configurations

When(/^'Operator' observes the list of 'Inbound Data Source'$/, async () => {
    await inboundPage.inboundDataSource.isPresent();
});

Then(/^'Operator' can see a list of existing 'Inbound Data Source' items$/, async () => {
    await inboundPage.inboundDataSource.isDisplayed();
});

//@DSDS-42 Test user can see a status of the last process run in Inbound configurations

Given(/^There is at least one process run for selected 'Inbound Data Source'$/, async () => {
    //expect(inboundPage.processCount).to.equal(1);
    await expect(element.all(by.xpath("//mercer-table/table/tbody/tr")).count()).to.eventually.above(0);
});

When(/^'Operator' observes selected 'Inbound Data Source'$/, async () => {
    await inboundPage.inboundDataSource.isDisplayed();
});

Then(/^'Operator' can see 'Last Running Date and Time' in 'Last Running' column$/, async () => {
    let row = element.all(by.xpath("//mercer-table/table/tbody/tr"));
    let cells = row.all(by.tagName('td'));

    let regex = /\w{3} \d{1,2}, \d{4}, \d{1,2}:\d{1,2}:\d{1,2} (PM|AM)/g;
    

    let cellTexts : string[] = await cells.map(function (elm) {
        return elm.getText();
    });

    cellTexts = cellTexts.filter(item => item.match(regex));

    expect(cellTexts.length).to.above(0);
});


//@DSDS-42 Test user can filter feeds by status

Given(/^There is "(.*?)" of 'Inbound Data Source' items in "(.*?)"$/, async (number, status) => {
    throw new PendingException();
});

When(/^'Operator' clicks on a "(.*?)" 'Filter' icon$/, async (color) => {
    throw new PendingException();
});

Then(/^'Operator' can see a "(.*?)" 'Inbound Data Source' items with selected 'Feed Status'$/, async (count) => {
    throw new PendingException();
});

//@DSDS-42 Test user can see a status dashboard

When(/^'Operator' observes the 'Chart' circles$/, async () => {
    throw new PendingException();
});

Then(/^'Operator' observes a "(.*?)" 'Chart' circle$/, async (circle_count) => {
    throw new PendingException();
});

Then(/^'Operator' can see a "(.*?)" of 'Inbound Data Source' items in 'Chart'$/, async (count) => {
    throw new PendingException();
});

//@SLA

Given(/^At least one Inbound configuration exists in the system$/, async () => {
    throw new PendingException();
});

When(/^Operator lands on the 'Dashboard' page$/, async () => {
    throw new PendingException();
});

When(/^Operator clicks on the 'Inbound' tab in top menu$/, async () => {
    throw new PendingException();
});

When(/^Operator observes the list$/, async () => {
    throw new PendingException();
});

Then(/^Operator can see a list of existing configurations$/, async () => {
    throw new PendingException();
});

//@History same as above for now