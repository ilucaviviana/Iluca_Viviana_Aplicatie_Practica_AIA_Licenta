import { Component, OnInit } from '@angular/core';
import { AuthDetail } from '../shared/auth-detail.model';
import { AuthDetailService } from '../shared/auth-detail.service';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';


@Component({
  selector: 'app-eu-page',
  templateUrl: './eu-page.component.html',
  styleUrls: ['./eu-page.component.css']
})
export class EuPageComponent implements OnInit {

  utilizator!: AuthDetail;

  jwtHelperService = new JwtHelperService();
  constructor(private user: AuthDetailService,  private router: Router) { }
   ngOnInit() {
    const token = localStorage.getItem('token');

    if (token) {
      const decodedToken = this.jwtHelperService.decodeToken(token);
      const userId = decodedToken.sub;

      this.user.getUtilizator(userId).subscribe({
        next: (utilizator) => {
          this.utilizator = utilizator;
        },
        error: (response) => {
          console.log(response);
        }
      });
    } else {
      console.log('Token is null');
    }
  }
  editUtilizator(userId: number) {
    const url = `edit/${userId}`;
    return this.router.navigateByUrl(url);
  }

}






