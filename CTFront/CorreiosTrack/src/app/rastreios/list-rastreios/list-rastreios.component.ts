import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {RastreioService} from "../../services/rastreio-service.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-list-rastreios',
  templateUrl: './list-rastreios.component.html',
  styleUrls: ['./list-rastreios.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class ListRastreiosComponent implements OnInit {

  rastreiosData = [{
    Id:"",
    Nome:"",
    Codigo:"",
    Status:""
  }];

  constructor(private rastreioService: RastreioService, private router: Router) { }

  ngOnInit() {
    this.retrieveTracksData();
  }

  retrieveTracksData() {
    this.rastreioService.getAllTracks().subscribe((res) => {
      this.rastreiosData = res;
    });
  }

  addButton() {
    this.router.navigateByUrl("/rastreio/insert");
  }

}
