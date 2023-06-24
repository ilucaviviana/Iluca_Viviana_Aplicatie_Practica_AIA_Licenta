import { Component, OnInit, OnChanges } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { ApometreDetailService } from '../shared/apometre-detail.service';
import { Response } from 'express';
import { Router } from '@angular/router';
import { SharedService } from '../shared.service';
import { ApometreDetail } from '../shared/apometre-detail.model';
import { JudetDetail } from '../shared/judet-detail.model';
import { OrasDetail } from '../shared/oras-detail.model';
import { StradaDetail } from '../shared/strada-detail.model';
import { BlocDetail } from '../shared/bloc-detail.model';
import { HttpClient } from '@angular/common/http';
import { LocatiiDetailService } from '../shared/locatii-detail.service';
import { DetailPageComponent } from '../detail-page/detail-page.component';
import { ActivatedRoute } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AlreadySubmittedDialogComponent } from '../alert/already-submitted-dialog/already-submitted-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { TarifeDetailService } from '../shared/tarife-detail.service';
import { PlataDetailService } from '../shared/plata-detail.service';
import { PlataDetail } from '../shared/plata-detail.model';
import { LocatiiDetail } from '../shared/locatii-detail.model';


@Component({
  selector: 'app-citire-page',
  templateUrl: './citire-page.component.html',
  styleUrls: ['./citire-page.component.css']
})
export class CitirePageComponent implements OnInit, OnChanges {
  selectedOras!: any;
  selectedJudet!: any;
  selectedStrada!: any;
  selectedBloc!: any;
  locations: any;
  orase: OrasDetail[] = [];
  judete: JudetDetail[] = [];
  strada: StradaDetail[] = [];
  bloc: BlocDetail[] = [];
  apometre: any;
  locationId!: number;
  userApometreData: ApometreDetail[] = [];

  ngOnInit() {
    this.fetchUserApometreData();
  }

  ngOnChanges() {
    this.fetchUserApometreData();
  }

  ApometruForm: FormGroup;

    constructor(private dialog: MatDialog,private tarifeService: TarifeDetailService,
    private plataService: PlataDetailService,
      private route: ActivatedRoute, public locatie: LocatiiDetailService,private router: Router, private service : SharedService, public apometreService: ApometreDetailService, private http: HttpClient, private formBuilder: FormBuilder) {
        this.ApometruForm = this.formBuilder.group({
            Ap1: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(4)]],
            Ap2: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(4)]],
            Ap3: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(4)]],
            Ap4: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(4)]],
            TransmitereData: [new Date()] // Default to current date
        }, {
          validator: this.compareApometruValues
        });

    }

    compareApometruValues(group: FormGroup) {
      let Ap1 = group?.get('Ap1')?.value;
      let Ap2 = group?.get('Ap2')?.value;
      let Ap3 = group?.get('Ap3')?.value;
      let Ap4 = group?.get('Ap4')?.value;

      if ((Ap1 && Ap3) && (Ap1 <= Ap3)) {
        return { 'Ap1LessThanAp3': true };
      }

      if ((Ap2 && Ap4) && (Ap2 <= Ap4)) {
        return { 'Ap2LessThanAp4': true };
      }

      return null;
    }


    formatDate(date: Date): string {
      const day = ('0' + date.getDate()).slice(-2);
      const month = ('0' + (date.getMonth() + 1)).slice(-2);
      const year = date.getFullYear();

      return `${year}-${month}-${day}`;
  }
   jwtHelperService = new JwtHelperService();
   TransmitereData: Date = new Date();
   Apometru() {
    if (this.ApometruForm.valid) {
        const token = localStorage.getItem('token');

        if (token) {
            const decodedToken = this.jwtHelperService.decodeToken(token);
            const iduser = decodedToken.sub;

            this.apometreService.getLatestApometre(iduser).subscribe(response => {
                let apometre: ApometreDetail = this.ApometruForm.value;
                apometre.Iduser = iduser;
                apometre.TransmitereData = new Date(this.formatDate(new Date())); // Assign the formatted date as a Date object

                if (response.message) {
                    console.log(response.message); // Handle the case when there's no data yet

                    this.apometreService.addApometre(apometre).subscribe(response => {
                        console.log(response);
                        // Do something after successful response
                        this.fetchTarifeAndCalculate(apometre);
                    }, error => {
                        console.error(error);
                        // Handle error
                    });
                } else {
                    const latestApometre = response as ApometreDetail;
                    const latestDate = new Date(latestApometre.TransmitereData);
                    const currentDate = new Date();

                    if (latestDate.getMonth() === currentDate.getMonth() &&
                        latestDate.getFullYear() === currentDate.getFullYear()) {
                          this.dialog.open(AlreadySubmittedDialogComponent);
                        //window.alert('You have already submitted data for this month');
                        // You can show a message to the user, disable the submit button, etc.
                    } else {
                        this.apometreService.addApometre(apometre).subscribe(response => {
                            console.log(response);
                            // Do something after successful response
                            this.fetchTarifeAndCalculate(apometre);
                        }, error => {
                            console.error(error);
                            // Handle error
                        });
                    }
                }
            }, error => {
                console.error(error);
                // Handle error
            });
        } else {
            console.log('Token is null');
        }
    } else {
        console.log('Apometre invalid');
    }
}
invoice!: PlataDetail;

fetchTarifeAndCalculate(apometre: ApometreDetail) {
  const token = localStorage.getItem('token');
  if (token) {
    const decodedToken = this.jwtHelperService.decodeToken(token);
    const iduser = decodedToken.sub;
    this.tarifeService.getTarife().subscribe(tarifeData => {
      const tarife = tarifeData[0];
      //const locatii = this.locatii.nrlocatari;
      // Calculate the fees based on Apometru and Tarife data
      const ApaUsage: number = (parseFloat(apometre.Ap1) - parseFloat(apometre.Ap3)) + (parseFloat(apometre.Ap2) - parseFloat(apometre.Ap4));
      const TotalApa: number = (ApaUsage * parseFloat(tarife.PretApa))*3;
      const TotalRetim: number = parseFloat(tarife.PretRetim);
      const TotalAdmin: number = parseFloat(tarife.PretAdmin);
      const TotalCuratenie: number = parseFloat(tarife.PretCuratenie);
      // Create an invoice and send it to the backend
      const invoice = {
        IdUtilizator: iduser,
        TotalApa,
        TotalRetim,
        TotalAdmin,
        TotalCuratenie,
        ApaUsage,
        RetimUsage: TotalRetim,
        AdminUsage: TotalAdmin,
        CuratenieUsage: TotalCuratenie,
        TransmitereData: new Date()
      };
      this.plataService.saveInvoiceData(invoice).subscribe(invoiceResponse => {
        console.log(invoiceResponse);
        this.router.navigate(['/plata']);
      }, error => {
        console.error(error);
      });
    }, error => {
      console.error(error);
    });
  } else {
    console.log('Token is null');
  }
}

fetchUserApometreData() {
  const token = localStorage.getItem('token');
  if (token) {
    const decodedToken = this.jwtHelperService.decodeToken(token);
    const iduser = decodedToken.sub;

    this.apometreService.getApometreDataByUserId(iduser).subscribe(response => {
      if (response.message) {
        console.log(response.message); // Handle the case when there's no data yet
      } else {
        this.userApometreData = response;  // save the fetched data
      }
    }, error => {
      console.error(error);
      // Handle error
    });
  } else {
    console.log('Token is null');
  }
}

  get Ap1(): FormControl{
    return this.ApometruForm.get("Ap1") as FormControl;
  }
  get Ap2(): FormControl{
    return this.ApometruForm.get("Ap2") as FormControl;
  }
  get Ap3(): FormControl{
    return this.ApometruForm.get("Ap3") as FormControl;
  }
  get Ap4(): FormControl{
    return this.ApometruForm.get("Ap4") as FormControl;
  }

}
