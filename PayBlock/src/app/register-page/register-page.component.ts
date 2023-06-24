import { Component, OnInit } from '@angular/core';
import { Input, Output, EventEmitter } from '@angular/core';
import { IUtilizator } from '../utilizator.interface';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthDetailService } from '../shared/auth-detail.service';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit {
  repeatPass: string='none';
  displayMsg: string | null | undefined = '';
  isAccountCreated: boolean | null | undefined = false;

  constructor(private router: Router, private authDetailService: AuthDetailService ) {
    this.displayMsg = '';
  }

  registerSubmited() {
    const tip = this.registerForm.value.tip!;
    const nume = this.registerForm.value.nume!;
    const prenume = this.registerForm.value.prenume!;
    const email = this.registerForm.value.email!;
    const parola = this.registerForm.value.parola!;
    const cnp = this.registerForm.value.cnp!;
    const telefon = this.registerForm.value.telefon!;

    if(this.registerForm.valid && this.Parola.value == this.Confirmare.value)
    {
      console.log(this.registerForm.valid);
      this.repeatPass='none'
      this.router.navigateByUrl('sign');
      this.authDetailService.registerUser([tip, nume, prenume, email, parola, cnp, telefon]).subscribe(res => {if (res == 'Success') {
        this.displayMsg = 'Contul a fost creat cu succes!';
        this.isAccountCreated = true;
      } else if (res == 'Deja exista!') {
        this.displayMsg = 'Contul deja exista! Incearca cu un alt email.';
        this.isAccountCreated = false;
      } else {
        this.displayMsg = 'Ceva nu a mers bine!';
        this.isAccountCreated = false;
      }

      });
    } else{
       this.repeatPass='inline';
      }
  }

  ngOnInit() { }

  registerForm = new FormGroup({
    tip: new FormControl("", [Validators.required]),
    nume: new FormControl("", [Validators.required, Validators.minLength(2), Validators.pattern('[a-zA-Z].*')]),
    prenume: new FormControl("",  [Validators.required, Validators.minLength(2), Validators.pattern('[a-zA-Z].*')]),
    email: new FormControl("",  [Validators.required, Validators.email]),
    parola: new FormControl("",  [Validators.required, Validators.minLength(5), Validators.maxLength(15)]),
    confirmare: new FormControl("",  [Validators.required]),
    cnp: new FormControl("",  [Validators.required, Validators.minLength(13), Validators.maxLength(13), Validators.pattern('[0-9].*')]),
    telefon: new FormControl("", [Validators.required, Validators.minLength(10), Validators.maxLength(10), Validators.pattern('[0-9].*')])
  });

  get Tip(): FormControl{
    return this.registerForm.get("tip") as FormControl;
  }

  get Nume(): FormControl{
    return this.registerForm.get("nume") as FormControl;
  }

  get Prenume(): FormControl{
    return this.registerForm.get("prenume") as FormControl;
  }

  get Email(): FormControl{
    return this.registerForm.get("email") as FormControl;
  }

  get Parola(): FormControl{
        return this.registerForm.get("parola") as FormControl;
      }
    get Confirmare(): FormControl{
        return this.registerForm.get("confirmare") as FormControl;
      }
      get cnp(): FormControl{
        return this.registerForm.get("cnp") as FormControl;
      }
      get Tel(): FormControl{
        return this.registerForm.get("telefon") as FormControl;
      }

    }
