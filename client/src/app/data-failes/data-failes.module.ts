import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MercerOSModule } from 'merceros-ui-components';
import { DataFailesComponent } from './data-failes.component';
import { FailesTableComponent } from './failes-table/failes-table.component';

@NgModule({
  declarations: [
    DataFailesComponent,
    FailesTableComponent
  ],
  imports: [
    CommonModule,
    MercerOSModule
  ],
  exports: [
    DataFailesComponent,
    FailesTableComponent
  ]
})
export class DataFailesModule { }
