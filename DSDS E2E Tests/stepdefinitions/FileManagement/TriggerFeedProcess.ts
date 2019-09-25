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


//File Management: Trigger feed process

//@DSDS-55 Test user can run file feed process by clicking a 'Run' button

When(/^Operator hits 'Run' next to the data feed$/, async () => {
    let row = element.all(by.xpath("//mercer-table/table/tbody/tr"));
    let cells = row.all(by.tagName('td'));

    let regex = /\w{3} \d{1,2}, \d{4}, \d{1,2}:\d{1,2}:\d{1,2} (PM|AM)/g;
    

    let cellTexts : string[] = await cells.map(function (elm) {
        return elm.getText();
    });

    cellTexts = cellTexts.filter(item => item.match(regex));

    InboundPageObject.lastRun = cellTexts[0];


    let links = row.all(by.tagName('a')); 
    await links.get(2).click();
    
});

Then(/^Operator can see a new 'Last Run' record on the Dashboard page for selected 'Data Source'$/, async () => {

    let row = element.all(by.xpath("//mercer-table/table/tbody/tr"));
    let cells = row.all(by.tagName('td'));

    let regex = /\w{3} \d{1,2}, \d{4}, \d{1,2}:\d{1,2}:\d{1,2} (PM|AM)/g;
    

    let cellTexts : string[] = await cells.map(function (elm) {
        return elm.getText();
    });

    cellTexts = cellTexts.filter(item => item.match(regex));
    
    expect(InboundPageObject.lastRun).to.be.not.equal(cellTexts[0]);
    InboundPageObject.lastRun = cellTexts[0];
});

Then(/^Operator can see a new historical record in 'Invalid Records' log$/, async () => {
    let rows = element.all(by.xpath("//mercer-table/table/tbody/tr"));
    let links = rows.all(by.tagName('a')); 
    let first = await links.first().getAttribute("href");

    await browser.get(first);



    let row = element.all(by.xpath("//mercer-table/table/tbody/tr"));
    let cells = row.all(by.tagName('td'));

    let regex = /\w{3} \d{1,2}, \d{4}, \d{1,2}:\d{1,2}:\d{1,2} (PM|AM)/g;
    

    let cellTexts : string[] = await cells.map(function (elm) {
        return elm.getText();
    });

    cellTexts = cellTexts.filter(item => item.match(regex));

    expect(InboundPageObject.lastRun).to.be.equal(cellTexts[0]);
});
