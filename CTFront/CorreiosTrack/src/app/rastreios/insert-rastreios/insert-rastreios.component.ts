import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {RastreioService} from "../../services/rastreio-service.service";

@Component({
  selector: 'app-insert-rastreios',
  templateUrl: './insert-rastreios.component.html',
  styleUrls: ['./insert-rastreios.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class InsertRastreiosComponent implements OnInit {

  trackForm: FormGroup;
  dataForm = {
    'Nome':'',
    'Codigo':''
  }

  constructor(private fb: FormBuilder, private trackService: RastreioService) {
    this.trackForm = fb.group({
      'name':[null, Validators.compose([Validators.maxLength(50), Validators.required])],
      'code':[null, Validators.compose([Validators.pattern('^[A-Za-z]{2}[0-9]{9}[A-Za-z]{2}$'), Validators.required])]
    });
  }

  submitForm(form) {
    this.dataForm.Nome = form.name;
    this.dataForm.Codigo = form.code;
    this.insertTrackForm();
  }

  insertTrackForm() {
    this.trackService.insertTrack(this.dataForm).subscribe( (res) => {
      console.log(res);
    }, (err) => {
      console.log(err);
    });
  }

  ngOnInit() {
  }

}
