import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {ListRastreiosComponent} from "./list-rastreios/list-rastreios.component";


const rastreioRoutes: Routes = [
  { path: 'rastreio',  component: ListRastreiosComponent },
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
