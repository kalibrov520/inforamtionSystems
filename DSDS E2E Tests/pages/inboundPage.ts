import { $, ElementFinder, by, element, ElementArrayFinder } from "protractor";



export class InboundPageObject {
    public searchTextBox: ElementFinder;
    public searchButton: ElementFinder;
    public cleanButton: ElementFinder;
    public static rowCount: number;

    public inboundTab: ElementFinder;
    public inboundDataSource: ElementFinder;
    //public inboundDataLastRunning: string[];
    //public processCount: number;

    

    constructor() {
        this.searchTextBox = element(by.css(".search-input"));
        this.searchButton = $("ds-data-search > div > button");
        this.cleanButton = $("button[alt='Clean']");


        this.inboundTab = element(by.cssContainingText("a", "Inbound"));
        this.inboundDataSource = $("ds-data-table");
        //this.getProcessNumberCount();
        //this.getDataLastRunning();
        

    }


    // async getProcessNumberCount() {
    //     this.processCount = await element.all(by.xpath("//mercer-table/table/tbody/tr")).count();
    // };

    // async getDataLastRunning() {
    //     let row: ElementFinder = element.all(by.repeater('item in items.list')).first();
    //     let cells = row.all(by.tagName('td'));

    //     let regex = new RegExp('/\w{3} \d{1,2}, \d{4}, \d{1,2}:\d{1,2}:\d{1,2} (PM|AM)/');

    //     let cellTexts : string[] = await cells.map(function (elm) {
    //         return elm.getText();
    //     });


    //     this.inboundDataLastRunning = cellTexts.filter(item => item.match(regex));
    // };
}
