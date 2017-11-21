import { Injectable } from '@angular/core';
import { Configuration} from "./app.constants";
import { Http, Headers, Response } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class RastreioService {

  private url;

  constructor(private configuration: Configuration, private http: Http){
    this.url = configuration.ServerWithApiUrl + "Rastreios";
  }

  getAllTracks() {
    return this.http.get(this.url, {headers: this.getHeaders()}).map((res: Response) => res.json());
  }

  insertTrack(body) {
    return this.http.post(this.url, body, {headers:this.getHeaders()});
  }

  deleteTrack(id) {
    const deleteUrl = this.url + "/" + id;
    return this.http.delete(deleteUrl, {headers:this.getHeaders()});
  }

  getTrack(id) {
    const getUrl = this.url + "/" + id;
    return this.http.get(getUrl, {headers:this.getHeaders()});
  }

  updateTrack(body) {
    const putUrl = this.url + "/" + body.Id;
    return this.http.put(putUrl, body, {headers:this.getHeaders()});

  }

  private getHeaders(){
    let headers = new Headers();
    headers.append('Accept', 'application/json');
    return headers;
  }
}
