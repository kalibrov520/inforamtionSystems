import { Component, OnInit, ViewEncapsulation, ChangeDetectionStrategy, Input } from '@angular/core';
import { DataFeedDetails } from '../../models/dataFeed';

@Component({
  selector: 'ds-feed-table',
  templateUrl: './feed-table.component.html',
  styleUrls: ['./feed-table.component.scss'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FeedTableComponent implements OnInit {
  @Input() rowDataAll: DataFeedDetails[];

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

  rowData: DataFeedDetails[];

  constructor() { }

  ngOnInit() {
  }

  ngOnChanges(): void {
    this.rowData = this.rowDataAll;

    console.log(this.rowData);
  }

}
