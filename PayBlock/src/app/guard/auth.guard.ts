import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot,CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import { Observable } from 'rxjs';
import { AuthDetailService } from '../shared/auth-detail.service';



@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate{
  constructor(private auth: AuthDetailService, private router: Router){}

  canActivate(){

      return true;



  }

}
