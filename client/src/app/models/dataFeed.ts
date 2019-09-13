export interface IDataFeed {
    status: string;
    dataFeed: string;
    lastRunning: string;
    successRows: number;
    failedRows: number;
}