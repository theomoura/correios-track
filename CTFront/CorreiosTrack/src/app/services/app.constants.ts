import { Injectable } from '@angular/core';

@Injectable()
export class Configuration {
  public server = "http://localhost:59463/";
  public apiUrl = "api/";

  public ServerWithApiUrl = this.server + this.apiUrl;
}
