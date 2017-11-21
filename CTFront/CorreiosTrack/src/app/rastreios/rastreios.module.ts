import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { HttpModule } from '@angular/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import {ModalModule} from "ng2-modal";

import { ListRastreiosComponent } from './list-rastreios/list-rastreios.component';
import {RastreiosRoutingModule} from "./rastreios-routing.module";
import {AppComponent} from "../app.component";
import {RastreioService} from "../services/rastreio-service.service";
import { InsertRastreiosComponent } from './insert-rastreios/insert-rastreios.component';


@NgModule({
  declarations: [
    ListRastreiosComponent,
    InsertRastreiosComponent
  ],
  imports: [
    BrowserModule,
    RastreiosRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    ModalModule,
    HttpModule,
    NgbModule
  ],
  providers: [RastreioService]
})
export class RastreiosModule { }
