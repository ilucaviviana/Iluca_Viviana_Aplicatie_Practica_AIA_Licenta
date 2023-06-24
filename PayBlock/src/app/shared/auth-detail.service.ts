
/*import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map, tap } from 'rxjs/operators';
import { AuthDetail } from './auth-detail.model';


@Injectable({
  providedIn: 'root'
})
export class AuthDetailService {

  constructor(private http: HttpClient,private jwtHelper: JwtHelperService) {
    this.userPayload = this.decodedToken();

     }

  currentUser: BehaviorSubject<AuthDetail | null> = new BehaviorSubject<AuthDetail | null>(null);
  private baseUrl:string ='https://localhost:44316/api/Utilizator/';
  private userPayload: any;
 // private jwtHelper: JwtHelperService;

  registerUser(utilizatorObj: any){
    return this.http.post<any>(`${this.baseUrl}register`, utilizatorObj);
  }
  // loginUser(loginObj: any){
  //   return this.http.post<any>(`${this.baseUrl}login`, loginObj);
  // }
  loginUser(loginObj: any){
    return this.http.post<any>(`${this.baseUrl}login`, loginObj)
      .pipe(
        tap((res) => {
          this.storeToken(res.token);
          this.userPayload = this.decodedToken(); // Decode token after login
        })
      );
  }

  storeToken(tokenValue: string)
  {
    console.log("Token: ", tokenValue);
    localStorage.setItem('token', tokenValue)
  }

  getToken()
  {
    return localStorage.getItem('token')
  }

  isLoggedIn():boolean{
    return !!localStorage.getItem('token')
  }

  decodedToken() {
    const token = this.getToken();
    console.log("Token to decode: ", token);
    if (!token) { // Check if the token is null
      return null; // Or return some default value if necessary
    }
    return this.jwtHelper.decodeToken(token);
  }

  getfullNameFromToken(){
    if(this.userPayload)
    return this.userPayload.unique_name;
  }
  getRoleFromToken(){
    if(this.userPayload)
    return this.userPayload.role;
  }
  getUtilizator(id:number): Observable<AuthDetail> {
    return this.http.get<AuthDetail>(`${this.baseUrl}${id}`);
  }
}*/

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map, tap } from 'rxjs/operators';
import { AuthDetail } from './auth-detail.model';


@Injectable({
  providedIn: 'root'
})
export class AuthDetailService {

  constructor(private http: HttpClient) { }

  private userRoleSubject = new BehaviorSubject<string>('');
  userRole$ = this.userRoleSubject.asObservable();


  currentUser: BehaviorSubject<any> = new BehaviorSubject(null);
  readonly baseUrl ='https://localhost:44316/api/';

  jwtHelperService = new JwtHelperService();

  registerUser(utilizator: string[]): Observable<string> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', 'Bearer ' + token);
     return this.http.post(this.baseUrl + 'utilizator/register', {
      Tip: utilizator[0],
      Nume: utilizator[1],
      Prenume: utilizator[2],
      Email: utilizator[3],
      Parola: utilizator[4],
      Cnp: utilizator[5],
      Telefon: utilizator[6]
    }, { headers: headers, responseType: 'text' as const });
  }

  loginUser(utilizator: string[]): Observable<string> {
    return this.http.post(this.baseUrl + 'utilizator/login', {
      Tip: utilizator[0],
      Email: utilizator[1],
      Parola: utilizator[2],
    }, { responseType: 'text' }).pipe(
      map(token => {
        // set the token in local storage
        localStorage.setItem('token', token);
        // decode and log the token if it's valid
        const decodedToken = this.jwtHelperService.decodeToken(token);
        if (decodedToken) {
          console.log('Decoded token:', decodedToken);
          const userId = decodedToken.sub; // Assuming the user ID property is 'sub'
          console.log('User ID:', userId);

          // Get the user data from the API based on the user ID
          this.getUtilizator(Number(userId)).subscribe(
            user => {
              this.currentUser.next(user); // Store the user data in currentUser BehaviorSubject
              console.log('Logged-in user:', user);
            },
            error => {
              console.log('Error:', error);
            }
          );
        }
        return token;
      })
    );
  }

  setToken(token: string){
    localStorage.setItem("access_token", token);
  }

  loadCurentUser() {
    const token = localStorage.getItem("token");
    const userInfo = token ? this.jwtHelperService.decodeToken(token) : null;
    const data = userInfo ? {
      tip: userInfo.Tip,
      nume: userInfo.Nume,
      prenume: userInfo.Prenume,
      email: userInfo.Email,
      tel: userInfo.Telefon,
      cnp: userInfo.Cnp
    } : null;

    this.currentUser.next(data);

    if (data && data.tip) {
      this.userRoleSubject.next(data.tip === "0" ? "admin" : "user");
    }

    if (token) {
      const userId = userInfo?.sub;
      this.getUtilizator(Number(userId)).subscribe(
        (user) => {
          this.currentUser.next(user);
        },
        (error) => {
          console.log("Error:", error);
        }
      );
    }
  }





  getUtilizator(userId: number): Observable<any> {
    const url = `${this.baseUrl}utilizator/${userId}`;
    return this.http.get(url).pipe(
      tap(user => this.currentUser.next(user))
    );
  }




  updateUtilizator(utilizator: AuthDetail): Observable<any> {
    const url = `${this.baseUrl}utilizator/${utilizator.Id}`;
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', 'Bearer ' + token);
    return this.http.put(url, utilizator, { headers: headers }); // Send full user data
  }

  postLocatii(locatii: any): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', 'Bearer ' + token);
    return this.http.post(this.baseUrl + 'locatii', locatii, { headers: headers });
  }



}


