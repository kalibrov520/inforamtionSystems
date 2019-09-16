import { $, ElementFinder, by, element } from "protractor";



export class InboundPageObject {
    public searchTextBox: ElementFinder;
    public searchButton: ElementFinder;
    public cleanButton: ElementFinder;
    public static rowCount: number;

    constructor() {
        this.searchTextBox = element(by.css(".search-input"));
        this.searchButton = $("ds-data-search > div > button");
        this.cleanButton = $("button[alt='Clean']");
    }
}
