import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MercerOSModule } from 'merceros-ui-components';

import { AppRoutingModule } from '../app-routing.module';

import { DashboardComponent } from './dashboard.component';
import { DataTableComponent } from './data-table/data-table.component';
import { StatusCircleComponent } from './status-circle/status-circle.component';
import { StatusFilterComponent } from './status-filter/status-filter.component';
import { StatusPresentationComponent } from './status-presentation/status-presentation.component';
import { DataSearchComponent } from './data-search/data-search.component';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

@NgModule({
  declarations: [
    DashboardComponent,
    DataTableComponent,
    StatusCircleComponent,
    StatusFilterComponent,
    StatusPresentationComponent,
    DataSearchComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    MercerOSModule,
    AppRoutingModule,
    HttpModule
  ],
  exports: [
    DashboardComponent,
    DataTableComponent,
    StatusCircleComponent,
    StatusFilterComponent
  ]
})
export class DashboardModule { }
