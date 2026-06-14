import { Injectable } from '@angular/core';

import {
  HttpClient
} from '@angular/common/http';

import {
  Observable
} from 'rxjs';

import {
  Client
} from '../models/client';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  private apiUrl =
    'https://wms-api-dishika-f8dtfnghfqezbxf5.centralindia-01.azurewebsites.net/api/Client';

  constructor(
    private http: HttpClient
  ) {}

  getClients():
  Observable<Client[]> {

    return this.http.get<Client[]>(
      this.apiUrl
    );

  }

  getClientById(id:number){

    return this.http.get<Client>(
      `${this.apiUrl}/${id}`
    );

  }

  createClient(client:Client){

    return this.http.post(
      this.apiUrl,
      client
    );

  }

  updateClient(
    id:number,
    client:Client
  ){

    return this.http.put(
      `${this.apiUrl}/${id}`,
      client
    );

  }

  deleteClient(id:number){

    return this.http.delete(
      `${this.apiUrl}/${id}`
    );

  }

}
