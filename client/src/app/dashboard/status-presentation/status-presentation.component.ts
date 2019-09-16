import { Component, OnInit, Input, ChangeDetectionStrategy } from '@angular/core';
import { IDataFeed } from '../../models/dataFeed';

@Component({
  selector: 'ds-status-presentation',
  templateUrl: './status-presentation.component.html',
  styleUrls: ['./status-presentation.component.scss'],
})
export class StatusPresentationComponent implements OnInit {
  @Input() allDataFeeds: IDataFeed[];
  failed: number;
  success: number;
  late: number;
  inactive: number;
  all: number;
  
  constructor() { }

  ngOnInit() {}

  ngOnChanges(): void {
    this.failed= this.allDataFeeds ? this.allDataFeeds.filter(data => data.status === "failed").length : 0;
    this.success=  this.allDataFeeds ? this.allDataFeeds.filter(data => data.status === "success").length : 0;
    this.late= this.allDataFeeds ? this.allDataFeeds.filter(data => data.status === "late").length : 0;
    this.inactive= this.allDataFeeds ? this.allDataFeeds.filter(data => data.status === "inactive").length: 0;
    this.all= this.allDataFeeds ?  this.allDataFeeds.length : 0;
  }

}
