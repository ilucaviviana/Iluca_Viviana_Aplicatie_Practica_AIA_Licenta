import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { OrasDetail } from './oras-detail.model';
import { Observable } from 'rxjs';
import { JudetDetail } from './judet-detail.model';

@Injectable({
  providedIn: 'root'
})
export class OrasDetailService {

  readonly baseUrl ='https://localhost:44316/api';

  constructor(private http: HttpClient) { }

  getOrase(): Observable<OrasDetail[]> {
    return this.http.get<OrasDetail[]>(`${this.baseUrl}/oras`);
  }


}
