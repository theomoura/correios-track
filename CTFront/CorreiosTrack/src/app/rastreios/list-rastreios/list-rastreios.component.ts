import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {RastreioService} from "../../services/rastreio-service.service";
import {Router} from "@angular/router";
import {ModalModule} from "ng2-modal";

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
  selectedRastreio: any;

  constructor(private rastreioService: RastreioService, private router: Router) { }

  ngOnInit() {
    this.retrieveTracksData();
  }

  retrieveTracksData() {
    this.rastreioService.getAllTracks().subscribe((res) => {
      this.rastreiosData = res;
    });
  }

  deleteTrack(id) {
    this.rastreioService.deleteTrack(id).subscribe((res) => {
      if (res.ok) {
        this.retrieveTracksData();
      } else {
      }
    });
  }

  setSelected(rastreio) {
    this.selectedRastreio = rastreio;
  }

  editTrack(rastreio) {
    this.router.navigate(['/rastreio/edit', rastreio.Id]);
  }

  addButton() {
    this.router.navigateByUrl("/rastreio/insert");
  }

}
