import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'ds-data-search',
  templateUrl: './data-search.component.html',
  styleUrls: ['./data-search.component.scss'],
})
export class DataSearchComponent implements OnInit {
  _searchFilter: string;

  get searchFilter(): string {
    return this._searchFilter;
  }
  set searchFilter(value: string) {
    this._searchFilter = value;
  }

  @Output() searchClicked: EventEmitter<string> = new EventEmitter<string>();

  constructor() { }

  onSearchButtonClicked(): void {
    console.log(this.searchFilter);
    this.searchClicked.emit(`${this.searchFilter}`);
  }

  clearFilter() {
    this.searchFilter = "";
    this.onSearchButtonClicked();
  }

  ngOnInit() {
  }

}
