import { NgModule }              from '@angular/core';
import { RouterModule, Routes }  from '@angular/router';
import {ListRastreiosComponent} from "./rastreios/list-rastreios/list-rastreios.component";


const appRoutes: Routes = [
  { path: '', component: ListRastreiosComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(
      appRoutes
    )
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule {}
