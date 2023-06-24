import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApartamentDetail } from './apartament-detail.model';

@Injectable({
  providedIn: 'root'
})
export class ApartamentDetailService {

  readonly baseUrl ='https://localhost:44316/api';

  constructor(private http: HttpClient) { }


  getApartament(): Observable<ApartamentDetail[]> {
    return this.http.get<ApartamentDetail[]>(`${this.baseUrl}/apartament`);
  }
}
