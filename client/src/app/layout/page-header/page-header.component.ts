import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'ds-page-header',
  templateUrl: './page-header.component.html',
  styleUrls: ['./page-header.component.scss'],
})
export class PageHeaderComponent implements OnInit {

  @Input() public pageTitle: string;
  @Input() public pageSubtitle: string;

  constructor() { }

  ngOnInit() {
  }

}
