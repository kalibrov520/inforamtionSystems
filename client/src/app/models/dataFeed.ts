export interface IDataFeed {
    status: string;
    dataFeed: string;
    lastRunning: string;
    successRows: number;
    failedRows: number;
}

export class DataFeed implements IDataFeed {
    status: string;
    dataFeed: string;
    lastRunning: string;
    successRows: number;
    failedRows: number;

    constructor(values: Object = {}) {
        Object.assign(this,values);
    }
}