import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MercerOSModule } from 'merceros-ui-components';
import { DataFeedComponent } from './data-feed.component';
import { CalendarRangeComponent } from './calendar-range/calendar-range.component';
import { FeedTableComponent } from './feed-table/feed-table.component';
import { AppRoutingModule } from '../app-routing.module';

@NgModule({
  declarations: [
    DataFeedComponent,
    CalendarRangeComponent,
    FeedTableComponent
  ],
  imports: [
    CommonModule,
    MercerOSModule,
    AppRoutingModule
  ],
  exports: [
    DataFeedComponent,
    CalendarRangeComponent,
    FeedTableComponent
  ]
})
export class DataFeedModule { }
