import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'ds-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  @Input() dataFeedName: string;
  @Input() dateRunning: string;

  constructor() { }

  ngOnInit() {
  }

}
