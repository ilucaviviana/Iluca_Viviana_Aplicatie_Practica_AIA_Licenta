import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthDetail } from '../shared/auth-detail.model';
import { AuthDetailService } from '../shared/auth-detail.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-edit-utilizator',
  templateUrl: './edit-utilizator.component.html',
  styleUrls: ['./edit-utilizator.component.css']
})
export class EditUtilizatorComponent implements OnInit {

  utilizator!: AuthDetail;
  jwtHelperService = new JwtHelperService();

  constructor(
    private route: ActivatedRoute,
    private user: AuthDetailService,
    private router: Router
  ) { }

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

  saveUtilizator() {
    if (this.utilizator) {
      this.user.updateUtilizator(this.utilizator).subscribe({ // Provide full user data
        next: () => {
          console.log('Utilizator updated successfully');
          this.router.navigate(['eu']);
        },
        error: (response) => {
          console.log(response);
        }
      });
    }
  }


}
