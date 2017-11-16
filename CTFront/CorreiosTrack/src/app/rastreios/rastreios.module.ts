import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { HttpModule } from '@angular/http';

import { ListRastreiosComponent } from './list-rastreios/list-rastreios.component';
import {RastreiosRoutingModule} from "./rastreios-routing.module";
import {AppComponent} from "../app.component";
import {RastreioService} from "../services/rastreio-service.service";


@NgModule({
  declarations: [
    ListRastreiosComponent
  ],
  imports: [
    BrowserModule,
    RastreiosRoutingModule,
    HttpModule,
    NgbModule
  ],
  providers: [RastreioService]
})
export class RastreiosModule { }
