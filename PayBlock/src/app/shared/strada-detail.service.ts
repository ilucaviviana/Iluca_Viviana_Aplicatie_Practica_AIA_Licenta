import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { StradaDetail } from './strada-detail.model';

@Injectable({
  providedIn: 'root'
})
export class StradaDetailService {

  readonly baseUrl ='https://localhost:44316/api';

  constructor(private http: HttpClient) { }


  getStrada(): Observable<StradaDetail[]> {
    return this.http.get<StradaDetail[]>(`${this.baseUrl}/strada`
    );
  }
}
