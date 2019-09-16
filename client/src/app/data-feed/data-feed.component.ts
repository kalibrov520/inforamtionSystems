import { Component, OnInit } from '@angular/core';
import { PaginationInfo } from 'merceros-ui-components';
import { ActivatedRoute, Router } from '@angular/router';
import { DataFeedService } from '../services/dataFeed.service';

@Component({
  selector: 'ds-data-feed',
  templateUrl: './data-feed.component.html',
  styleUrls: ['./data-feed.component.scss'],
})
export class DataFeedComponent implements OnInit {
  deploymentId: string;
 
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

  constructor(private dataFeedService: DataFeedService, private activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.deploymentId = this.activatedRoute.snapshot.paramMap.get('id');
    
  }

}
