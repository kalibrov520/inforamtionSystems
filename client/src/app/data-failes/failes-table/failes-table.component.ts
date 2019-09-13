import { Component, OnInit, ViewEncapsulation, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'ds-failes-table',
  templateUrl: './failes-table.component.html',
  styleUrls: ['./failes-table.component.scss'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FailesTableComponent implements OnInit {
  
  headerData: any[] = [
    {
      name: 'Fund',
      column: 'fund',
    },
    {
      name: 'Plan',
      column: 'plan',
    },
    {
      name: 'Asset',
      column: 'asset',
    },
    {
      name: 'Share Balance',
      column: 'shareBalance',
    },
    {
      name: 'NAV',
      column: 'nav',
    },
    {
      name: 'Error',
      column: 'error',
    },
    {
      name: 'Edit',
      column: 'edit',
    },
    {
      name: 'Delete',
      column: 'delete',
    },
  ];

  rowData: any[] = [
    {
      dataId: '1',
      fund: 'Fund name #1',
      plan: 'Plan name #1',
      asset: '5',
      shareBalance: '23%',
      nav: '18',
      error: 'Unable to identify a Plan Name',
    },
    {
      dataId: '2',
      fund: 'Fund name #2',
      plan: 'Plan name #2',
      asset: '2',
      shareBalance: '10%',
      nav: '21',
      error: 'Unable to identify a Plan Name',
    },
    {
      dataId: '3',
      fund: 'Fund name #3',
      plan: 'Plan name #3',
      asset: '8',
      shareBalance: '70%',
      nav: '11',
      error: '0 balance record',
    },
  ];

  constructor() { }

  ngOnInit() {
  }

}
