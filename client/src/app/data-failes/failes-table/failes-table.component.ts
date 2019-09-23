import { Component, OnInit, ViewEncapsulation, ChangeDetectionStrategy, Input, ChangeDetectorRef } from '@angular/core';
import { element } from '@angular/core/src/render3';
import { jsonpCallbackContext } from '@angular/common/http/src/module';

@Component({
  selector: 'ds-failes-table',
  templateUrl: './failes-table.component.html',
  styleUrls: ['./failes-table.component.scss'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FailesTableComponent implements OnInit {
  @Input() dataFeedFailes: string[];
  
  headerData: any[];

  rowData: any[] = [];

  constructor( ) { }

  ngOnInit() { }

  ngOnChanges(): void {
    if(!this.dataFeedFailes || this.dataFeedFailes.length == 0)
    {
      return;
    }

    let headerKeys = Object.keys(JSON.parse(this.dataFeedFailes[0]));

    this.headerData = headerKeys.map(element => ({ name: element, column: element}));

    this.rowData = this.dataFeedFailes.map(element => JSON.parse(element));
  }

}
