import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {RastreioService} from "../../services/rastreio-service.service";

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

  constructor(private rastreioService: RastreioService) { }

  ngOnInit() {
    this.retrieveTracksData();
  }

  retrieveTracksData() {
    this.rastreioService.getAllTracks().subscribe((res) => {
      this.rastreiosData = res;
    });
  }

}
