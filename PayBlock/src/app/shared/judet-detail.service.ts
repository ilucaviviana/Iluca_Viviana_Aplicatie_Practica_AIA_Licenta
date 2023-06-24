import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JudetDetail } from './judet-detail.model';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class JudetDetailService {


  readonly baseUrl ='https://localhost:44316/api';

  constructor(private http: HttpClient) { }


  getJudete(): Observable<JudetDetail[]> {
    return this.http.get<JudetDetail[]>(`${this.baseUrl}/judet`);
  }
}
