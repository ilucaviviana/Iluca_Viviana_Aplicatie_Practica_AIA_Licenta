import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApometreDetail } from './apometre-detail.model';


@Injectable({
  providedIn: 'root'
})
export class ApometreDetailService {

  readonly apiUrl = 'https://localhost:44316/api';

  constructor(private http: HttpClient) { }

  addApometre(apometre: ApometreDetail): Observable<ApometreDetail> {
    return this.http.post<ApometreDetail>(`${this.apiUrl}/Apometre`, apometre);
  }

  getLatestApometre(Iduser: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/Apometre/latest/${Iduser}`);
}

getApometreDataByUserId(Iduser: number): Observable<any> {
  return this.http.get<any>(`${this.apiUrl}/Apometre/${Iduser}`);
}

}
