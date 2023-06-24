import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserAuthService {
private unique_name$ = new BehaviorSubject<string>("");
private tip$ = new BehaviorSubject<string>("");

constructor() { }

public getFullNameFrom(){
  return this.unique_name$.asObservable();
}

public setFullNameFrom(unique_name: string){
  this.unique_name$.next(unique_name)
}

}
