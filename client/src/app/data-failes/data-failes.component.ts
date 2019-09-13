import { Component, OnInit, ViewEncapsulation, ChangeDetectionStrategy } from '@angular/core';
import { PaginationInfo } from 'merceros-ui-components';

@Component({
  selector: 'ds-data-failes',
  templateUrl: './data-failes.component.html',
  styleUrls: ['./data-failes.component.scss'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DataFailesComponent implements OnInit {

  public paginationInfo: PaginationInfo = {
    offset: 0,
    limit: 10,
    limits: [10, 20, 30],
    totalCount: 3
  };

  public updatePage(model) {
    this.paginationInfo.limit = model.limit;
    this.paginationInfo.offset = model.offset;
  }

  constructor() { }

  ngOnInit() {
  }

}
