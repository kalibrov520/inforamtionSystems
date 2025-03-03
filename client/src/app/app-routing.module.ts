import { NgModule } from '@angular/core';
import { RouterModule, Routes, PreloadAllModules } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { FeedComponent } from './layout/feed/feed.component';
import { FailesComponent } from './layout/failes/failes.component';

const routes: Routes = [
  { path: '', component: LayoutComponent },
  { path: 'dataFeed/:id/:name', component: FeedComponent },
  { path: 'failes/:id/:name', component: FailesComponent },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { useHash: false, preloadingStrategy: PreloadAllModules })
  ],
  exports: [
    RouterModule
  ],
  declarations: []
})
export class AppRoutingModule { }
