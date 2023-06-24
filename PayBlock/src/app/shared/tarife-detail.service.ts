import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TarifeDetail } from './tarife-detail.model';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class TarifeDetailService {

  readonly baseUrl ='https://localhost:44316/api';

  constructor(private http: HttpClient, private jwtHelperService: JwtHelperService) { }

  getTarife(): Observable<TarifeDetail[]> {
    return this.http.get<TarifeDetail[]>(`${this.baseUrl}/Tarife`);
  }

  createTarife(tarife: TarifeDetail): Observable<TarifeDetail> {
    return this.http.post<TarifeDetail>(`${this.baseUrl}/Tarife`, tarife);
  }

  updateTarife(id: number, tarife: TarifeDetail): Observable<TarifeDetail> {
    return this.http.put<TarifeDetail>(`${this.baseUrl}/Tarife/${id}`, tarife);
  }

  getUserID(): number | null {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken = this.jwtHelperService.decodeToken(token);
      const userId = decodedToken.sub;
      return Number(userId);
    }
    return null;
  }

  getLastSubmissionDate(id: number): Observable<TarifeDetail> {
    return this.http.get<TarifeDetail>(`${this.baseUrl}/Tarife/date/${id}`);
  }

}
