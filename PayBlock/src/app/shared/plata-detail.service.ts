import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Observable } from 'rxjs';
import { PlataDetail } from './plata-detail.model';

@Injectable({
  providedIn: 'root'
})
export class PlataDetailService {

  private data = new BehaviorSubject(null);
  constructor(private http: HttpClient, private jwtHelperService: JwtHelperService) { }

  readonly baseUrl ='https://localhost:44316/api';

  setData(value: any) {
    this.data.next(value);
  }

  getData() {
    return this.data.asObservable();
  }

  saveInvoiceData(invoice: PlataDetail): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/Factura`, invoice);
  }

}

