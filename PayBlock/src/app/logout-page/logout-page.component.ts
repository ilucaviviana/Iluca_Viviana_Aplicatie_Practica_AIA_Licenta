import { Component, OnInit } from '@angular/core';
import { AuthDetailService } from '../shared/auth-detail.service';
import { UserAuthService } from '../shared/user-auth.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ChangeDetectorRef } from '@angular/core';
import { ViewChild, ElementRef } from '@angular/core';


@Component({
  selector: 'app-logout-page',
  templateUrl: './logout-page.component.html',
  styleUrls: ['./logout-page.component.css']
})
export class LogoutPageComponent implements OnInit {

  @ViewChild('logoutModal') logoutModal!: ElementRef;

  public unique_name: string="";
  constructor( private authService: AuthDetailService, private userauth: UserAuthService, private router : Router, private cdr: ChangeDetectorRef) { }

  userRole!: string;
  subscription!: Subscription;

  ngOnInit(): void {
    this.subscription = this.authService.userRole$.subscribe(
      (userRole) => {
        this.userRole = userRole;
        this.cdr.detectChanges();
      }
    );
    this.authService.loadCurentUser();
  }


  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngAfterViewInit(): void {
    // Manually remove the modal backdrop after it's hidden
    const logoutModalElement = document.getElementById('logout');
    logoutModalElement?.addEventListener('hidden.bs.modal', () => {
      const modalBackdrop = document.querySelector('.modal-backdrop');
      if (modalBackdrop) {
        modalBackdrop.remove();
      }
    });
  }

  logoutUser(){
    this.logoutModal.nativeElement.click();
    localStorage.removeItem('token');
    this.router.navigateByUrl('/information');

  }

}
