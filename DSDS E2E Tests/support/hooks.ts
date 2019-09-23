const { BeforeAll, Before, After, AfterAll, Status } = require("cucumber");
import * as fs from "fs";
import { browser } from "protractor";
import { config } from "../config/config";
import { async } from "q";

BeforeAll({timeout: 100 * 1000}, async () => {
    await browser.get(config.baseUrl);
    await browser.waitForAngularEnabled(true);
});


Before(async ()=> {
    await browser.waitForAngular();
});

After(async function(scenario) {
    if (scenario.result.status === Status.FAILED) {
        // screenShot is a base-64 encoded PNG
         const screenShot = await browser.takeScreenshot();
         this.attach(screenShot, "image/png");
    }
});

AfterAll({timeout: 100 * 1000}, async () => {
    await browser.quit();
});
