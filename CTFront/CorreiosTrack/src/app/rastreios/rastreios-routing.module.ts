import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {ListRastreiosComponent} from "./list-rastreios/list-rastreios.component";
import {InsertRastreiosComponent} from "./insert-rastreios/insert-rastreios.component";


const rastreioRoutes: Routes = [
  { path: 'rastreio',  component: ListRastreiosComponent },
  { path: 'rastreio/insert',  component: InsertRastreiosComponent },
  { path: 'rastreio/edit/:id',  component: InsertRastreiosComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(rastreioRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class RastreiosRoutingModule { }
