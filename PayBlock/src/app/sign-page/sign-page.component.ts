import { Component } from '@angular/core';
import { SharedService } from '../shared.service';
import { IUtilizator } from '../utilizator.interface';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthDetailService } from '../shared/auth-detail.service';


@Component({
  selector: 'app-sign-page',
  templateUrl: './sign-page.component.html',
  styleUrls: ['./sign-page.component.css']
})
export class SignPageComponent {

  isUserValid: boolean= false;

  constructor(private fb: FormBuilder, private router: Router, private service : SharedService, private loginService: AuthDetailService) { }

  ngOnInit(): void {

}

  Login() {
    const tip = this.loginForm.value.tip!;
    const email = this.loginForm.value.email!;
    const parola = this.loginForm.value.parola!;

    if (this.Tip.value == 0 && this.loginForm.valid) {
      console.log(this.loginForm.valid);


      this.loginService.loginUser([tip, email, parola]).subscribe(
        token => {
          console.log('Token:', token);
          // handle success
          this.loginService.setToken(token);
          this.loginService.loadCurentUser();
          this.isUserValid = true;
          this.router.navigate(['/admin']);
          alert('Logare cu succes');
        },
        error => {
          console.log('Error:', error);
          // handle error
          this.isUserValid = false;
          alert('Logare nefinalizata!');
        }
      );
    } else if (this.Tip.value == 1 && this.loginForm.valid) {
      this.loginService.loginUser([tip, email, parola]).subscribe(
        token => {
          console.log('Token:', token);
          // handle success
          this.loginService.setToken(token);
          this.loginService.loadCurentUser();
          this.isUserValid = true;
          this.router.navigateByUrl('detail');
        },
        error => {
          console.log('Error:', error);
          // handle error
          this.isUserValid = false;
          alert('Nu există un cont cu acest email. Vă rugăm să creați unul!');
        }
      );
    }
  }


 loginForm = new FormGroup({
    tip: new FormControl("", [Validators.required]),
    email: new FormControl("",  [Validators.required, Validators.email]),
    parola: new FormControl("",  [Validators.required, Validators.minLength(5), Validators.maxLength(15)])

  });

  get Tip(): FormControl{
    return this.loginForm.get("tip") as FormControl;
  }
  get Email(): FormControl{
    return this.loginForm.get("email") as FormControl;
  }
  get Parola(): FormControl{
    return this.loginForm.get("parola") as FormControl;
  }
}
