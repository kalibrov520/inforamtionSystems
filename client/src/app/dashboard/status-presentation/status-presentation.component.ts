import { Component, OnInit, Input } from '@angular/core';
import { DataFeedService } from '../../services/dataFeed.service';
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
  
  constructor(private dataFeedService: DataFeedService) { }

  ngOnInit() {
    this.failed= this.allDataFeeds.filter(data => data.status === "failed").length;
    this.success= this.allDataFeeds.filter(data => data.status === "success").length;
    this.late= this.allDataFeeds.filter(data => data.status === "late").length;
    this.inactive= this.allDataFeeds.filter(data => data.status === "inactive").length;
    this.all= this.allDataFeeds.length;
  }

}
