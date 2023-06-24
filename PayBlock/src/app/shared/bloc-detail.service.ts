import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { BlocDetail } from './bloc-detail.model';

@Injectable({
  providedIn: 'root'
})
export class BlocDetailService {

  readonly baseUrl ='https://localhost:44316/api';

  constructor(private http: HttpClient) { }


  getBloc(): Observable<BlocDetail[]> {
    return this.http.get<BlocDetail[]>(`${this.baseUrl}/bloc`);
  }
}
