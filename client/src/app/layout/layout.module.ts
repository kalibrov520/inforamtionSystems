import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MercerOSModule } from 'merceros-ui-components';
import { HeaderComponent } from './header/header.component';
import { PageHeaderComponent } from './page-header/page-header.component';
import { LayoutComponent } from './layout.component';
import { DashboardModule } from '../dashboard/dashboard.module';
import { FeedComponent } from './feed/feed.component';
import { DataFeedModule } from '../data-feed/data-feed.module';
import { FailesComponent } from './failes/failes.component';
import { DataFailesModule } from '../data-failes/data-failes.module';

@NgModule({
  declarations: [
    HeaderComponent,
    PageHeaderComponent,
    LayoutComponent,
    FeedComponent,
    FailesComponent,
  ],
  imports: [
    CommonModule,
    MercerOSModule,
    DashboardModule,
    DataFeedModule,
    DataFailesModule,
  ]
})
export class LayoutModule { }
