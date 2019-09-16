import { Component, OnInit, Input, HostListener, ViewEncapsulation } from '@angular/core';
import { IDataFeed } from '../../models/dataFeed';

@Component({
  selector: 'ds-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class DataTableComponent implements OnInit {
  
  @Input() filesList: string = "rowDataAll";
  @Input() searchFilter: string;
  @Input() statusFilter: string;
  @Input() rowDataAll: IDataFeed[];
  
  rowData : IDataFeed[];
  rowDataFailed: IDataFeed[];
  rowDataSuccess: IDataFeed[];
  rowDataLate: IDataFeed[];
  rowDataInactive: IDataFeed[];
  errorMessage: string;
  
  headerData: any[] = [
    {
      name: 'Status',
      column: 'status'
    },
    {
      name: 'Data Feed',
      column: 'dataFeed'
    },
    {
      name: 'Last Running',
      column: 'lastRunning'
    },
    {
      name: 'Success Rows',
      column: 'successRows'
    },
    {
      name: 'Failed Rows',
      column: 'failedRows'
    },
    {
      name: 'Settings',
      column: 'settings',
      disableSort: true,
    },
  ];

  constructor() { }

  ngOnInit(): void {
    this.rowData = this.rowDataAll;
  }

  ngOnChanges(): void {
    let dataFilteredByStatus = this.statusFilter === "all"
      ? this.rowDataAll : this.rowDataAll.filter(data => data.status === this.statusFilter);

    if (this.searchFilter) {
      this.rowData = dataFilteredByStatus.filter((data: IDataFeed) =>
        data.dataFeed.toLowerCase().includes(this.searchFilter));
    }
    else {
      this.rowData = dataFilteredByStatus
    }
  }

}
