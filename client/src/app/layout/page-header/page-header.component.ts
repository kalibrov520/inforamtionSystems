import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'ds-page-header',
  templateUrl: './page-header.component.html',
  styleUrls: ['./page-header.component.scss'],
})
export class PageHeaderComponent implements OnInit {
  pageTitle: string;
  pageSubtitle: string;
  constructor(private activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.pageTitle = this.activatedRoute.snapshot.paramMap.get('name');
    
    if (this.pageTitle === null)
      this.pageTitle = "Inbound"
  }

}
