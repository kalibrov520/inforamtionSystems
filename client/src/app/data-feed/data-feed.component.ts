import { Component, OnInit } from '@angular/core';
import { PaginationInfo } from 'merceros-ui-components';

@Component({
  selector: 'ds-data-feed',
  templateUrl: './data-feed.component.html',
  styleUrls: ['./data-feed.component.scss'],
})
export class DataFeedComponent implements OnInit {
 
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
