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


//File Management: Process run history

//@DSDS-41 Test user can view the history of process runs for a specific Data Source

When(/^Operator clicks on 'Inbound Data Source' item in the list$/, async () => {

    let rows = element.all(by.xpath("//mercer-table/table/tbody/tr"));
    let rowsFailed = await rows.all(by.className("mos-t-circle--alert")).count();

    let first;
    let links ; 
    if (rowsFailed > 0) {
        let rowsArray = await rows.getText();
        for (let i = 0; i < rowsArray.length; i++) {
            let words = rowsArray[i].split(" ");
            if (words[words.length - 1] !== "0") {
                links = rows.get(i);
                first = await links.element(by.tagName('a')).getAttribute("href");
                break;
            }
        }
    } else {
        links = rows.all(by.tagName('a'));
        first = await links.first().getAttribute("href");
    }

    await browser.get(first);
});

Then(/^Operator lands on  'Feed History' page$/, async () => {
    await expect(browser.getCurrentUrl()).to.eventually.match(/dataFeed/);
});

Then(/^Operator can see 'Process Run' log with following atributes: Status, Date, Success Rows, Failed Rows$/, async () => {
    let row = element.all(by.xpath("//mercer-table/table/thead/tr"));
    let text = await row.first().getText();

    expect(text).to.match(/Status Date Success Rows Failed Rows/);
});

//@DSDS-41 Test user can view the invalid records for specific Process Run

Given(/^A process is completed with 'Failed' status$/, async () => {
    let rows = element.all(by.xpath("//mercer-table/table/tbody/tr"));
    let rowsFailed = rows.all(by.className("mos-t-circle--alert"));

    expect(rowsFailed.count()).to.eventually.above(0);
});

When(/^Operator clicks on  the hyperlink with # of failed rows$/, async () => {
    let rows = element.all(by.xpath("//mercer-table/table/tbody/tr"));


    
    let links;
    let first;
    let flag = false;


    for (let i = 0; i < await rows.count(); i++) {
        links = rows.get(i).all(by.tagName('a'));

        for (let j = 0; j < await links.count(); j++) {
            let link = links.get(j);
            first = await link.getAttribute("href");
            
        
            if (first !== "javascript:;" && first !== "#") {
                flag = true;
                break;
            }
        }

        if (flag) {
            break;
        }
    }

    await browser.get(first);
});

Then(/^Operator can see 'Invalid Records' log$/, async () => {
    let row = element.all(by.xpath("//mercer-table/table/thead/tr"));
    let text = await row.first().getText();

    expect(text).to.match(/ISN FundID FundName MarketValue Shares NAV MarketDate ValidationDate/);
});
