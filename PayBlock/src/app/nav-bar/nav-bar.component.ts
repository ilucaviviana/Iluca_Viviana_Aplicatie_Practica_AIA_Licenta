import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthDetailService } from '../shared/auth-detail.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent {

  userRole: string = '';
  subscription!: Subscription;

  constructor(private loginService: AuthDetailService) { }

  ngOnInit() {
    this.subscription = this.loginService.userRole$.subscribe(role => {
      this.userRole = role;
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
