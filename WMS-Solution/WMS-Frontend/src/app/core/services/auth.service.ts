import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { LoginRequest } from '../models/login-request';
import { LoginResponse } from '../models/login-response';

import { environment }
from '../../../environments/environment';

// NOTE: Backend runs HTTP on 5161 (launchSettings.json). Avoid accidental HTTPS calls.
const apiBaseHttp = 'https://wms-api-dishika-f8dtfnghfqezbxf5.centralindia-01.azurewebsites.net/api';


@Injectable({
  providedIn:'root'
})
export class AuthService {

  // Use HTTP explicitly (backend uses HTTP at 5161 in launchSettings.json).
  private apiUrl = `${apiBaseHttp}/Auth`;


  constructor(
    private http:HttpClient
  ){}

  login(
    request:LoginRequest
  ):Observable<LoginResponse>{

    return this.http.post<LoginResponse>(
      `${this.apiUrl}/login`,
      request
    );

  }

  logout(){

    localStorage.clear();

  }

  saveToken(token:string){

    localStorage.setItem(
      'token',
      token
    );

  }

  getToken(){

    return localStorage.getItem(
      'token'
    );

  }

  isLoggedIn(){

    return !!this.getToken();

  }
  changePassword(data:any){

  return this.http.post(

    `${this.apiUrl}/change-password`,

    data

  );

}

}
