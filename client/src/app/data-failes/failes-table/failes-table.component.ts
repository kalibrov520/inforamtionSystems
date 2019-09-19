import { Component, OnInit, ViewEncapsulation, ChangeDetectionStrategy, Input } from '@angular/core';

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

  constructor() { }

  ngOnInit() { }

  ngOnChanges(): void {
    let headerKeys = Object.keys(JSON.parse(this.dataFeedFailes[0]));

    this.headerData = headerKeys.map(element => ({ name: element, column: element}));

    let result = this.dataFeedFailes.forEach(element => {
      let values  = Object.values(JSON.parse(element));

      this.rowData.push(JSON.parse(element));
    })
  }

}
