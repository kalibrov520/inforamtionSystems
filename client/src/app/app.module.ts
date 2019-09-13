import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MercerOSModule } from 'merceros-ui-components';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { StoreModule } from '@ngrx/store';
import {
  StoreRouterConnectingModule,
  // RouterState,
} from '@ngrx/router-store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { EffectsModule } from '@ngrx/effects';

// import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';

import { metaReducers, ROOT_REDUCERS } from './reducers';
import { LayoutModule } from './layout/layout.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { DataFeedModule } from './data-feed/data-feed.module';
import { DataFailesModule } from './data-failes/data-failes.module';

@NgModule({
  imports: [
    BrowserAnimationsModule,
    MercerOSModule.forRoot(),
    CommonModule,
    AppRoutingModule,
    StoreModule.forRoot(ROOT_REDUCERS, {
      metaReducers,
    }),
    StoreRouterConnectingModule.forRoot({}),
    StoreDevtoolsModule.instrument({
      name: 'NgRx Store DevTools',
      logOnly: environment.production
    }),
    EffectsModule.forRoot([]),
    DashboardModule,
    LayoutModule,
    DataFeedModule,
    DataFailesModule,
  ],
  declarations: [
    AppComponent,
  ],
  exports: [
    AppComponent,
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
