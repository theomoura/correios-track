import { BrowserModule } from '@angular/platform-browser';
import { RastreiosModule } from './rastreios/rastreios.module';
import { NgModule } from '@angular/core';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';


import { AppComponent } from './app.component';
import {AppRoutingModule} from "./app-routing.module";
import {Configuration} from "./services/app.constants";


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    RastreiosModule,
    AppRoutingModule,
    NgbModule.forRoot(),
  ],
  providers: [Configuration],
  bootstrap: [AppComponent]
})
export class AppModule { }
