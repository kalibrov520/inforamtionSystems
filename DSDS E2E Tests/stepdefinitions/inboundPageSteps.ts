import { InboundPageObject } from "../pages/inboundPage";
import { config } from "../config/config";
import { browser, element, by } from "protractor";
const { Given, When, Then } = require("cucumber");
const chai = require("chai").use(require("chai-as-promised"));
const expect = chai.expect;

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

