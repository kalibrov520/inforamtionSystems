import { InboundPageObject } from "../../pages/inboundPage";
import { config } from "../../config/config";
import { browser, element, by, ExpectedConditions, protractor } from "protractor";
import { TestObject } from "protractor/built/driverProviders";
import { AssertionError } from "assert";
const { Given, When, Then, PendingException } = require("cucumber");
const chai = require("chai").use(require("chai-as-promised"));
const chaiHttp = require('chai-http');
chai.use(chaiHttp);
const expect = chai.expect;
const rp = require('request-promise');

var inboundPage: InboundPageObject = new InboundPageObject();



//File Management: List of inbound configurations
//@DSDS-42 Test user can see a list of Inbound configurations

When(/^'Operator' observes the list of 'Inbound Data Source'$/, async () => {
    expect(await inboundPage.inboundDataSource.isPresent()).to.be.true;
});

Then(/^'Operator' can see a list of existing 'Inbound Data Source' items$/, async () => {
    expect(await inboundPage.inboundDataSource.isDisplayed()).to.be.true;
});

//@DSDS-42 Test user can see a status of the last process run in Inbound configurations

Given(/^There is at least one process run for selected 'Inbound Data Source'$/, async () => {
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

Given(/^There is "(.*?)" of 'Inbound Data Source' items in "(.*?)"$/,{timeout: 2 * 5000} ,async (number, status) => { 
    await browser.get("http://spb-mdspoc01.internal.corp:702/"); //site with static data
    await browser.waitForAngular();
    inboundPage = new InboundPageObject();


    let charts = element.all(by.css(".status-presentation-row > .status-files > .status-visual-presentation > .files-amount > .number"));
    let chartsText : string[] = await charts.map(function (elm) {
        return elm.getText();
    });

    switch(status) {
        case "Failed":
            expect(number).to.be.equal(chartsText[0]);
            break;
        case "Success":
            expect(number).to.be.equal(chartsText[1]);
            break;
        case "Late":
            expect(number).to.be.equal(chartsText[2]);
            break;
        case "Inactive":
            expect(number).to.be.equal(chartsText[3]);
            break;
        case "All":
            expect(number).to.be.equal(chartsText[4]);
            break;
        default:
            throw new Error("There is no such status");
            break;
    }
});

When(/^'Operator' clicks on a "(.*?)" 'Filter' icon$/, async (color) => {
    let filters = element.all(by.xpath("//ds-status-filter/div/ul/li/a"));
    await browser.wait(ExpectedConditions.presenceOf(filters.get(0)), 5000, 'Element taking too long to appear in the DOM');
    console.log(await filters);

    switch(color) {
        case "Red":
            filters.get(0).click();
            break;
        case "Green":
            filters.get(1).click();
            break;
        case "Yellow":
            filters.get(2).click();
            break;
        case "Grey":
            filters.get(3).click();
            break;
        case "Blue":
            filters.get(4).click();
            break;
        default:
            throw new Error("There is no such filter");
            break;
    };

    
});

Then(/^'Operator' can see a "(.*?)" 'Inbound Data Source' items with selected 'Feed Status'$/, async (count) => {
    await browser.waitForAngular();
    let rows = element.all(by.xpath("//mercer-table/table/tbody/tr"));
    await browser.wait(ExpectedConditions.presenceOf(rows.get(0)), 5000, 'Element taking too long to appear in the DOM');
    expect(await rows.count()).to.be.equal(parseInt(count));
});

//@DSDS-42 Test user can see a status dashboard

When(/^'Operator' observes the 'Chart' circles$/, async () => {
    let chart = element(by.css(".status-presentation-row"));
    expect(await chart.isDisplayed()).to.be.true;
});

Then(/^'Operator' observes a "(.*?)" 'Chart' circle$/, async (color) => {
    let elem ;
    switch(color) {
        case "Red":
            elem = await element(by.css(".cutoff-status-red"));
            break;
        case "Green":
            elem = await element(by.css(".cutoff-status-green"));
            break;
        case "Yellow":
            elem = await element(by.css(".cutoff-status-yellow"));
            break;
        case "Grey":
            elem = await element(by.css(".cutoff-status-gray"));
            break;
        case "Blue":
            elem = await element(by.css(".cutoff-status-blue"));
            break;
        default:
            throw new Error("There is no such filter");
            break;
    };

    expect(elem).to.be.not.equal(undefined);
    inboundPage.inboundChart = elem;
});

Then(/^'Operator' can see a "(.*?)" of 'Inbound Data Source' items in 'Chart'$/, async (count) => {
    let text = await inboundPage.inboundChart.getText();
    expect(text[0]).to.be.equal(count);
});
