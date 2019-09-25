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


//File Management: List of inbound configurations
//File Management: Trigger feed process
//File Management: Process run history
//Background

Given(/^At least one 'Inbound Data Source' exists in the system$/, async () => { 


    let options = {
        uri: 'http://spb-mdspoc01.internal.corp:8080/engine-rest/process-definition',
        json: true 
    };
     
    await rp(options)
        .then(function (repos) {
            expect(repos.length).above(0);
        });


});

Given(/^'Operator' lands on the 'Dashboard' page$/, async () => {
    await browser.get(config.baseUrl);
});

Given(/^'Operator' clicks on the 'Inbound' tab in top menu$/, async () => {
    await inboundPage.inboundTab.click();
});
