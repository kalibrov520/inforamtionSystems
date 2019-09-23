import { Component, OnInit, ViewEncapsulation, ChangeDetectionStrategy, ChangeDetectorRef, Input } from '@angular/core';
import { PaginationInfo } from 'merceros-ui-components';
import { DataFeedService } from '../services/dataFeed.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from "@angular/common";

@Component({
  selector: 'ds-data-failes',
  templateUrl: './data-failes.component.html',
  styleUrls: ['./data-failes.component.scss'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DataFailesComponent implements OnInit {
  @Input() runId: string;
  dataFeedFailes: string[];
  errorMessage: string;

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

  constructor(private location: Location ,private dataFeedService: DataFeedService, private cdr: ChangeDetectorRef ,private activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.runId = this.activatedRoute.snapshot.paramMap.get('id');
    debugger;

    this.dataFeedService.getDataFeedFailesInfo(this.runId).subscribe({
      next: dataFeedFailes => {
        this.dataFeedFailes = dataFeedFailes;
        this.cdr.detectChanges();
      },
      error: err => this.errorMessage = err
    });
  }

  returnToDataFeedDetails() {
    this.location.back();
  }

}
