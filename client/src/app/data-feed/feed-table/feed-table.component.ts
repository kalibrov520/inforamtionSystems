import { Component, OnInit, ViewEncapsulation, ChangeDetectionStrategy, Input } from '@angular/core';

@Component({
  selector: 'ds-feed-table',
  templateUrl: './feed-table.component.html',
  styleUrls: ['./feed-table.component.scss'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FeedTableComponent implements OnInit {
  @Input() rowDataAllId: string;

  headerData: any[] = [
    {
      name: 'Status',
      column: 'status',
    },
    {
      name: 'Date',
      column: 'date',
    },
    {
      name: 'Success Rows',
      column: 'successRows',
      disableSort: true,
    },
    {
      name: 'Failed Rows',
      column: 'failedRows',
      disableSort: true,
    },
    {
      name: 'File',
      column: 'file',
      disableSort: true,
    },
  ];

  rowData: any[] = [
    {
      dataId: '1',
      status: 'failed',
      date: '01-May-2019 4:00:34 AM',
      successRows: '500',
      failedRows: '1',
    },
    {
      dataId: '2',
      status: 'success',
      date: '01-May-2019 5:00:34 AM',
      lastRunning: '02/09/2019',
      successRows: '800',
      failedRows: '0',
    },
    {
      dataId: '3',
      status: 'late',
      date: '30-Apr-2019 4:00:34 AM',
      lastRunning: '02/09/2019',
      successRows: '430',
      failedRows: '0',
    },
  ];

  constructor() { }

  ngOnInit() {
  }

}
