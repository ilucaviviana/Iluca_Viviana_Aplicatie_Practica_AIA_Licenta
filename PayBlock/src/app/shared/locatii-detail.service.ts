
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { LocatiiDetail } from './locatii-detail.model';
import { JwtHelperService } from '@auth0/angular-jwt';
import { JudetDetail } from './judet-detail.model';
import { OrasDetail } from './oras-detail.model';
import { StradaDetail } from './strada-detail.model';
import { BlocDetail } from './bloc-detail.model';
import { ApartamentDetail } from './apartament-detail.model';


@Injectable({
  providedIn: 'root'
})
export class LocatiiDetailService {

  constructor(private http: HttpClient) { }
  readonly baseUrl = 'https://localhost:44316/api';

  currentUser: BehaviorSubject<any> = new BehaviorSubject(null);
  jwtHelperService = new JwtHelperService();


  getUserLocation(userId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/locatii/${userId}`);
  }

  private judetSource = new BehaviorSubject<JudetDetail | null>(null);
  judet$ = this.judetSource.asObservable();

  private orasSource = new BehaviorSubject<OrasDetail | null>(null);
  oras$ = this.orasSource.asObservable();

  private stradaSource = new BehaviorSubject<StradaDetail | null>(null);
  strada$ = this.stradaSource.asObservable();

  private blocSource = new BehaviorSubject<BlocDetail | null>(null);
  bloc$ = this.blocSource.asObservable();

  private apartamentSource = new BehaviorSubject<ApartamentDetail | null>(null);
  apartament$ = this.apartamentSource.asObservable();

  setJudet(judet: JudetDetail) {
    this.judetSource.next(judet);
  }

  setOras(oras: OrasDetail) {
    this.orasSource.next(oras);
  }

  setStrada(strada: StradaDetail) {
    this.stradaSource.next(strada);
  }

  setBloc(bloc: BlocDetail) {
    this.blocSource.next(bloc);
  }

  setApartament(apartament: ApartamentDetail) {
    this.apartamentSource.next(apartament);
  }


}

