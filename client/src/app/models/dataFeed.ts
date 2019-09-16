export interface IDataFeed {
    deploymentId: string;
    status: string;
    dataFeed: string;
    lastRunning: string;
    successRows: number;
    failedRows: number;
}

export class DataFeed implements IDataFeed {
    deploymentId: string;
    status: string;
    dataFeed: string;
    lastRunning: string;
    successRows: number;
    failedRows: number;

    constructor(values: Object = {}) {
        Object.assign(this,values);
    }
}