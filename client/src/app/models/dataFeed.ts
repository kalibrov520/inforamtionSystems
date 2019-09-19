export interface IDataFeed {
    deploymentId: string;
    processDefinitionId: string;
    status: string;
    dataFeed: string;
    lastRunning: string;
    successRows: number;
    failedRows: number;
}

export class DataFeed implements IDataFeed {
    processDefinitionId: string;
    deploymentId: string;
    status: string;
    dataFeed: string;
    lastRunning: string;
    successRows: number;
    failedRows: number;

    constructor(values: Object = {}) {
        Object.assign(this, values);
    }
}

export class DataFeedDetails {
    runId: string;
    status: string;
    date: string;
    failedRows: number;
    successRows: number;
    constructor(values: Object = {}) {
        Object.assign(this, values);
    }
}