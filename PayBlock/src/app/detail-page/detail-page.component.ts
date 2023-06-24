import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { OrasDetailService } from '../shared/oras-detail.service';
import { OrasDetail } from '../shared/oras-detail.model';
import { JudetDetail } from '../shared/judet-detail.model';
import { JudetDetailService } from '../shared/judet-detail.service';
import { StradaDetailService } from '../shared/strada-detail.service';
import { StradaDetail } from '../shared/strada-detail.model';
import { BlocDetail } from '../shared/bloc-detail.model';
import { BlocDetailService } from '../shared/bloc-detail.service';
import { ApometreDetailService } from '../shared/apometre-detail.service';
import { ApometreDetail } from '../shared/apometre-detail.model';
import { LocatiiDetailService } from '../shared/locatii-detail.service';
import { AuthDetailService } from '../shared/auth-detail.service';
import { FormBuilder } from '@angular/forms';
import { Token } from '@angular/compiler';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LocatiiDetail } from '../shared/locatii-detail.model';
import { forkJoin } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { AlertDialogComponent } from '../alert/alert-dialog.component';
import { ApartamentDetail } from '../shared/apartament-detail.model';
import { ApartamentDetailService } from '../shared/apartament-detail.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-detail-page',
  templateUrl: './detail-page.component.html',
  styleUrls: ['./detail-page.component.css']
})
export class DetailPageComponent implements OnInit {
  selectedJudet!: any;
  selectedOras!: any;
  selectedStrada!: any;
  selectedBloc!: any;
  selectedApartament!: any;
  orase: OrasDetail[] = [];
  judete: JudetDetail[] = [];
  strada: StradaDetail[] = [];
  bloc: BlocDetail[] = [];
  apartament: ApartamentDetail[]=[];
  isLocationFound = false;
  isUserValid!: boolean;


  constructor(private router: Router, private dialog: MatDialog, public jwtHelper: JwtHelperService, private formBuilder: FormBuilder, private http: HttpClient, public service: OrasDetailService, public service2: JudetDetailService, public service3: StradaDetailService, public service4: BlocDetailService, public service5: ApartamentDetailService, public apometreService: ApometreDetailService, public locatii: LocatiiDetailService, public auth: AuthDetailService) {}

  ngOnInit(): void {
    const userId = Number(this.getUserId());
    forkJoin({
      judetData: this.http.get<JudetDetail[]>('https://localhost:44316/api/judet'),
      orasData: this.http.get<OrasDetail[]>('https://localhost:44316/api/oras'),
      stradaData: this.http.get<StradaDetail[]>('https://localhost:44316/api/strada'),
      blocData: this.http.get<BlocDetail[]>('https://localhost:44316/api/bloc'),
      apartamentData: this.http.get<ApartamentDetail[]>('https://localhost:44316/api/apartament'),
      userData: this.locatii.getUserLocation(userId)
    }).subscribe({
      next: data => {
        this.judete = data.judetData;
        this.orase = data.orasData;
        this.strada = data.stradaData;
        this.bloc = data.blocData;
        this.apartament = data.apartamentData;
        // call getUserLocation() only after all the HTTP requests have completed
        this.getUserLocation();
      },
      error: error => {
        console.error('Failed to fetch data:', error);
      }
    });
  }


 locationForm = new FormGroup({
    judet: new FormControl("",  [Validators.required]),
    oras: new FormControl("", [Validators.required]),
    strada: new FormControl("",  [Validators.required]),
    bloc: new FormControl("",  [Validators.required]),
    apartament: new FormControl("",  [Validators.required]),
    nrlocatari: new FormControl("",  [Validators.required, Validators.pattern("^[0-9]*$")]),
    nrapometre: new FormControl("", [Validators.required, Validators.pattern("^[0-9]*$")]),
  });

  get Oras(): FormControl{
    return this.locationForm.get("oras") as FormControl;
  }
  get Judet(): FormControl{
    return this.locationForm.get("judet") as FormControl;
  }
  get Strada(): FormControl{
    return this.locationForm.get("strada") as FormControl;
  }
  get Bloc(): FormControl{
    return this.locationForm.get("bloc") as FormControl;
  }
  get Apartament(): FormControl{
    return this.locationForm.get("apartament") as FormControl;
  }
  get Locatari(): FormControl{
    return this.locationForm.get("nrlocatari") as FormControl;
  }
  get Apometre(): FormControl{
    return this.locationForm.get("nrapometre") as FormControl;
  }



  getUserId() {
    const token = localStorage.getItem('token');  // Get the token from local storage
    if (!token) {
      // Token not found in local storage
      // Handle this situation as appropriate for your application
      console.error('Token not found in local storage');
      return null;
    }
    const decodedToken = this.jwtHelper.decodeToken(token);
    return decodedToken.sub; // or whatever the ID field name is in your token
  }

  onLocationSelected() {
    if (this.isLocationFound) {
      console.log('User has already selected a location.');
      this.dialog.open(AlertDialogComponent);
      return;
    }
    if (this.locationForm.valid) {

        const locatii = this.locationForm.value;
        const userId = Number(this.getUserId()); // get this value from your user data
        if (!userId) {
            // handle case when userId is null or undefined
            console.error('User not found in local storage');
            return;
        }
        const locatiiWithUserId = { IdUser: userId,...locatii, nrlocatari: Number(locatii.nrlocatari),
          nrapometre: Number(locatii.nrapometre) }; // create a new object

          console.log('Locatie', locatiiWithUserId);
        this.auth.postLocatii(locatiiWithUserId).subscribe(
            (response) => {
                console.log(response);
            // disable form controls after saving
            this.locationForm.controls['judet'].disable();
            this.locationForm.controls['oras'].disable();
            this.locationForm.controls['strada'].disable();
            this.locationForm.controls['bloc'].disable();
            this.locationForm.controls['apartament'].disable();
            this.locationForm.controls['nrlocatari'].disable();
            this.locationForm.controls['nrapometre'].disable();
      },
            (error) => {
                console.error(error);
            }
        );
    } else {
        console.log('Form is invalid!');
    }
  }
  currentLocationId: number | undefined;

  getCurrentLocationId(): number | undefined {
    return this.currentLocationId;
  }

  getUserLocation() {
    const userId = Number(this.getUserId());
    this.locatii.getUserLocation(userId).subscribe(
      (response: any) => {
        if (response) {
          this.isLocationFound = true;
          this.locationForm.disable();
          if (response.Judet) {
            this.selectedJudet = this.judete.find(j => j.NumeJudet.trim().toLowerCase() == response.Judet.trim().toLowerCase());
            if (this.selectedJudet)
            {
                this.locationForm.controls['judet'].setValue(this.selectedJudet.NumeJudet);
            }
            else
            {
                console.error('Could not find Judet with NumeJudet:', response.Judet);
            }
          }
          if (response.Oras) {
            this.selectedOras = this.orase.find(o => o.NumeOras.trim().toLowerCase() == response.Oras.trim().toLowerCase());
            if (this.selectedOras) {
                this.locationForm.controls['oras'].setValue(this.selectedOras.NumeOras);
            } else {
                console.error('Could not find Oras with NumeOras:', response.Oras);
            }
          }
          if (response.Strada) {
            this.selectedStrada = this.strada.find(s => s.NumeStrada.trim().toLowerCase() == response.Strada.trim().toLowerCase());
            if (this.selectedStrada) {
                this.locationForm.controls['strada'].setValue(this.selectedStrada.NumeStrada);
            } else {
                console.error('Could not find Strada with NumeStrada:', response.Strada);
            }
          }
          if (response.Bloc) {
            this.selectedBloc = this.bloc.find(b => b.NumarBloc.trim().toLowerCase() == response.Bloc.trim().toLowerCase());
            if (this.selectedBloc) {
                this.locationForm.controls['bloc'].setValue(this.selectedBloc.NumarBloc);
            } else {
                console.error('Could not find Bloc with NumarBloc:', response.Bloc);
            }
          }
          if (response.Apartament) {
            this.selectedApartament = this.apartament.find(a => a.NumarApartament == response.Apartament);
            if (this.selectedApartament) {
                this.locationForm.controls['apartament'].setValue(this.selectedApartament.NumarApartament);
            } else {
                console.error('Could not find Apartament with NumarApartament:', response.Apartament);
            }
          }
          if (response.Nrlocatari) {
            this.locationForm.controls['nrlocatari'].setValue(response.Nrlocatari);
          }
          if (response.Nrapometre) {
            this.locationForm.controls['nrapometre'].setValue(response.Nrapometre);
          }
        }
      },
      (error) => {
        console.error('Failed to fetch user location:', error);
      }
    );
  }

}
