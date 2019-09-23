import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'ds-status-circle',
  templateUrl: './status-circle.component.html',
  styleUrls: ['./status-circle.component.scss'],
})
export class StatusCircleComponent implements OnInit {
  
  @Input() status: string;

  constructor() { }

  ngOnInit() {
  }

}