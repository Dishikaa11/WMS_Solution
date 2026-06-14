import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { UserLogin }
from '../models/user-login';

@Injectable({
  providedIn:'root'
})
export class UserLoginService {

  private apiUrl =
  'https://wms-api-dishika-f8dtfnghfqezbxf5.centralindia-01.azurewebsites.net/api/UserLogin';

  constructor(
    private http: HttpClient
  ){}

  getUsers():
  Observable<UserLogin[]>{

    return this.http.get<UserLogin[]>(
      this.apiUrl
    );

  }

  getUserById(id:number){

    return this.http.get<UserLogin>(
      `${this.apiUrl}/${id}`
    );

  }

  createUser(user:UserLogin){

    return this.http.post(
      this.apiUrl,
      user
    );

  }

  updateUser(
    id:number,
    user:UserLogin
  ){

    return this.http.put(
      `${this.apiUrl}/${id}`,
      user
    );

  }

  deleteUser(id:number){

    return this.http.delete(
      `${this.apiUrl}/${id}`
    );

  }

}
