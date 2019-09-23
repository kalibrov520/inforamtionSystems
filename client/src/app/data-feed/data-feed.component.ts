import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { PaginationInfo } from 'merceros-ui-components';
import { ActivatedRoute, Router } from '@angular/router';
import { DataFeedService } from '../services/dataFeed.service';
import { DataFeedDetails } from '../models/dataFeed';

@Component({
  selector: 'ds-data-feed',
  templateUrl: './data-feed.component.html',
  styleUrls: ['./data-feed.component.scss'],
})
export class DataFeedComponent implements OnInit {
  deploymentId: string;
  dataFeedDetails: DataFeedDetails[];
  errorMessage: string;
  dataFeedName: string;
 
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

  constructor(private dataFeedService: DataFeedService, private cdr: ChangeDetectorRef ,private activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.deploymentId = this.activatedRoute.snapshot.paramMap.get('id');

    this.dataFeedService.getDataFeedDetails(this.deploymentId).subscribe({
      next: dataFeedDetails => {
        this.dataFeedDetails = dataFeedDetails;
        this.cdr.detectChanges();
      },
      error: err => this.errorMessage = err
    });
  }

}
