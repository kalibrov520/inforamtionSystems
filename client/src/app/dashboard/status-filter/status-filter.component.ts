import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'ds-status-filter',
  templateUrl: './status-filter.component.html',
  styleUrls: ['./status-filter.component.scss'],
})
export class StatusFilterComponent implements OnInit {

  activeEl: string = 'all';
  @Output() activeFilter: EventEmitter<string> = new EventEmitter<string>();

  constructor() { }

  ngOnInit() {
  }

  ngOnChanges(): void { }

  onStatusFilterClick(status: string) {
    this.activeFilter.emit(status);
  }

}
