import {Component, OnInit, ViewContainerRef, ViewEncapsulation} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {RastreioService} from "../../services/rastreio-service.service";
import {ActivatedRoute, Router} from "@angular/router";
import { ToastsManager } from 'ng2-toastr/ng2-toastr';
import {isNullOrUndefined} from "util";

@Component({
  selector: 'app-insert-rastreios',
  templateUrl: './insert-rastreios.component.html',
  styleUrls: ['./insert-rastreios.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class InsertRastreiosComponent implements OnInit {

  trackForm: FormGroup;
  dataForm = {
    'Id':0,
    'Status':'',
    'Nome':'',
    'Codigo':''
  }

  constructor(private fb: FormBuilder, private trackService: RastreioService,
              private router: Router, private route: ActivatedRoute,private toastManager: ToastsManager,
              vcr: ViewContainerRef) {
    this.toastManager.setRootViewContainerRef(vcr);
    this.trackForm = fb.group({
      'name':[null, Validators.compose([Validators.maxLength(50), Validators.required])],
      'code':[null, Validators.compose([Validators.pattern('^[A-Za-z]{2}[0-9]{9}[A-Za-z]{2}$'), Validators.required])]
    });
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      let id = params['id'];
      if (!isNullOrUndefined(id)) {
        this.getTrackForEdition(id);
      }
    });
    this.toastManager.success("opa","opa")
  }

  submitForm(form) {
    this.dataForm.Nome = form.name;
    this.dataForm.Codigo = form.code;
    if (this.dataForm.Id != 0) {
      this.updateTrack();
    } else {
      this.insertTrackForm();
    }
  }

  getTrackForEdition(id) {
    this.trackService.getTrack(id).subscribe((res) => {
      if (res.ok) {
        const data = res.json();
        this.dataForm.Id = data.Id;
        this.dataForm.Status = data.Status;
        this.trackForm.setValue({
          'name': data.Nome,
          'code': data.Codigo
        });
      }
    });
  }

  updateTrack() {
    this.trackService.updateTrack(this.dataForm).subscribe((res) => {
      if (res.ok) {
        this.router.navigateByUrl("/rastreio");
        this.toastManager.success("Rastreamento alterado com sucesso",'Success');
      } else {
        this.toastManager.error("Erro ao alterar rastreamento", "Erro");
      }
    }, (err) => {
      this.toastManager.error("Erro ao alterar rastreamento", "Erro");
      console.log(err);
    })
  }

  insertTrackForm() {
    this.trackService.insertTrack(this.dataForm).subscribe( (res) => {
      if (res.ok) {
        this.router.navigateByUrl("/rastreio");
        this.toastManager.success("Rastreamento inserido com sucesso", 'Success');
      } else {
        this.toastManager.error("Erro ao inserir rastreamento", "Erro");
      }
    }, (err) => {
      this.toastManager.error("Erro ao inserir rastreamento", "Erro");
      console.log(err);
    });
  }
}
