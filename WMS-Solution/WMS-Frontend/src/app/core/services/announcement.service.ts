import { Injectable } from '@angular/core';

import {
  HttpClient
} from '@angular/common/http';

import {
  Observable
} from 'rxjs';

import {
  Announcement
} from '../models/announcement';

@Injectable({
  providedIn: 'root'
})
export class AnnouncementService {

  private apiUrl =
  'http://localhost:5161/api/Announcement';

  constructor(
    private http: HttpClient
  ) {}

  getAnnouncements():
  Observable<Announcement[]> {

    return this.http.get<Announcement[]>(
      this.apiUrl
    );

  }

  getAnnouncementById(id:number){

    return this.http.get<Announcement>(
      `${this.apiUrl}/${id}`
    );

  }

  createAnnouncement(data:any){

    return this.http.post(
      this.apiUrl,
      data
    );

  }

  updateAnnouncement(
    id:number,
    data:any
  ){

    return this.http.put(
      `${this.apiUrl}/${id}`,
      data
    );

  }

  deleteAnnouncement(id:number){

    return this.http.delete(
      `${this.apiUrl}/${id}`
    );

  }

}