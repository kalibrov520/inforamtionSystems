import { Component, OnInit } from '@angular/core';
import { PaginationInfo } from 'merceros-ui-components';
import { DataFeedService } from '../services/dataFeed.service';
import { IDataFeed } from '../models/dataFeed';

@Component({
  selector: 'ds-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
  searchFilter: string;
  statusFilter: string;
  rowDataAll: IDataFeed[];
  
  public paginationInfo: PaginationInfo = {
    offset: 0,
    limit: 10,
    limits: [10, 20, 30],
    totalCount: 2
  };

  public updatePage(model) {
    this.paginationInfo.limit = model.limit;
    this.paginationInfo.offset = model.offset;
  }

  onSearchClicked(message: string): void {
    this.searchFilter = message;
  }

  onFilterClicked(message: string): void {
    this.statusFilter = message;
  }
  
  constructor(private dataFeedService: DataFeedService) { }

  ngOnInit() {
    this.rowDataAll = this.dataFeedService.getData();
  }

}
