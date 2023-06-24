import { Router } from '@angular/router';
import { TarifeDetailService } from '../shared/tarife-detail.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TarifeDetail } from '../shared/tarife-detail.model';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthDetailService } from '../shared/auth-detail.service';
import { AuthDetail } from '../shared/auth-detail.model';
import { HttpClient } from '@angular/common/http';
import { LocatiiDetail } from '../shared/locatii-detail.model';
import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import moment from 'moment';
import 'chartjs-adapter-moment/dist/chartjs-adapter-moment.js';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-admin-page',
  templateUrl: './admin-page.component.html',
  styleUrls: ['./admin-page.component.css']
})

export class AdminPageComponent implements OnInit, AfterViewInit {
  @ViewChild('tarifeChart') tarifeChart!: ElementRef;
  isNewSubmission: boolean = false;


  ngAfterViewInit(): void {
    Chart.register(...registerables);
    this.generateTarifeChart();
}



  generateTarifeChart(): void {
    const chartElement = this.tarifeChart.nativeElement.getContext('2d');

    // Get the tarife data to plot on the chart
    this.tarifeService.getTarife().subscribe(data => {
      const labels = data.map(tarife => moment(tarife.TransmitereData).toDate());
      const pretRetimData = data.map(tarife => parseFloat(tarife.PretRetim));
      const pretCuratenieData = data.map(tarife => parseFloat(tarife.PretCuratenie));
      const pretAdminData = data.map(tarife => parseFloat(tarife.PretAdmin));
      const pretApaData = data.map(tarife => parseFloat(tarife.PretApa));

      // Create the chart
      new Chart(chartElement, {
        type: 'line',
        data: {
          labels: labels,
          datasets: [
            {
              label: 'Preț Retim',
              data: pretRetimData,
              borderColor: 'red',
              fill: false
            },
            {
              label: 'Preț Curatenie',
              data: pretCuratenieData,
              borderColor: 'green',
              fill: false
            },
            {
              label: 'Preț Admin',
              data: pretAdminData,
              borderColor: 'blue',
              fill: false
            },
            {
              label: 'Preț Apa',
              data: pretApaData,
              borderColor: 'orange',
              fill: false
            }
          ]
        },
        options: {
          responsive: true,
          maintainAspectRatio: false,
          scales: {
            x: {
              type: 'time',
              time: {
                tooltipFormat: 'MMM YYYY',
                unit: 'month',
                displayFormats: {
                  month: 'MMM YYYY'
                }
              }
            },
            y: {
              title: {
                display: true,
                text: 'Preț'
              }
            }
          }
        }
      });
    });
  }

  tarife = {
    IdTarife: null,
    Id: '',
    PretRetim: '',
    PretCuratenie: '',
    PretAdmin: '',
    PretApa: '',
    TransmitereData: new Date()
};

  constructor(private snackBar: MatSnackBar,private httpClient: HttpClient, private utilizatorService: AuthDetailService, private jwtHelperService: JwtHelperService, private formBuilder: FormBuilder, private tarifeService: TarifeDetailService) {

    this.TarifeForm = this.formBuilder.group({
      IdTarife: [0],
      PretRetim: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(10)]],
      PretCuratenie: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(10)]],
      PretAdmin: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(10)]],
      PretApa: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(10)]],
      TransmitereData: [new Date()] // Default to current date
  });
   }
   utilizators: AuthDetail[] = [];
   originalUtilizators: AuthDetail[] = [];
   locatie: LocatiiDetail[]=[];
   originalLocatie: LocatiiDetail[]=[];
   ngOnInit(): void {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken = this.jwtHelperService.decodeToken(token);
      const iduser = decodedToken.sub;
      // Call the newly added endpoint
      this.tarifeService.getLastSubmissionDate(iduser).subscribe(data => {
        // Set the last submission date
        this.tarife = data;
        // Check if the IdTarife is defined
        if (this.tarife.IdTarife) {
          this.isUpdate = true;
          // Set the values of the form controls
          this.TarifeForm.controls['PretRetim'].setValue(this.tarife.PretRetim);
          this.TarifeForm.controls['PretCuratenie'].setValue(this.tarife.PretCuratenie);
          this.TarifeForm.controls['PretAdmin'].setValue(this.tarife.PretAdmin);
          this.TarifeForm.controls['PretApa'].setValue(this.tarife.PretApa);
          this.TarifeForm.controls['IdTarife'].setValue(this.tarife.IdTarife);

          // Get the current date
          const now = new Date();
          // Get the last submission date
          if(this.tarife.TransmitereData){
            const lastSubmissionDate = new Date(this.tarife.TransmitereData);

            // Check if the last submission was in the current month
            if (lastSubmissionDate.getMonth() === now.getMonth() &&
              lastSubmissionDate.getFullYear() === now.getFullYear()) {
              // If yes, disable the form and set the isFormSubmitted flag to true
              this.TarifeForm.disable();
              this.isFormSubmitted = true;
            }
          }
        } else {
          console.error('Fetched Tarife does not have a valid IdTarife');
        }
      }, error => {
        // The record for this month does not exist
        console.error(error);
        // Enable the form for a new submission
        this.TarifeForm.enable();
        this.isNewSubmission = true;
      });

    this.httpClient.get<AuthDetail[]>('https://localhost:44316/api/Utilizator').subscribe(data => {
      this.utilizators = data.filter(user => user.Tip == 1);
      this.originalUtilizators = [...this.utilizators];  // copy the data to originalUtilizators
    });

    this.httpClient.get<LocatiiDetail[]>('https://localhost:44316/api/Locatii').subscribe(datas => {
      console.log(datas);
      this.locatie = datas;
      this.originalLocatie = [...this.locatie];
    });
  }
}

  searchbyId!: number;
  search(): void {
    if(this.searchbyId) {
        this.locatie = this.originalLocatie.filter(user => user.IdUser == this.searchbyId);
    } else {
        this.locatie = [...this.originalLocatie];
    }
}

  searchId!: number;
  searchById(): void {
    if(this.searchId) {
      this.utilizators = this.originalUtilizators.filter(user => user.Id == this.searchId);
    } else {
      this.utilizators = [...this.originalUtilizators];
    }
  }




  get PretRetim(): FormControl{
    return this.TarifeForm.get("PretRetim") as FormControl;
  }
  get PretCuratenie(): FormControl{
    return this.TarifeForm.get("PretCuratenie") as FormControl;
  }
  get PretAdmin(): FormControl{
    return this.TarifeForm.get("PretAdmin") as FormControl;
  }
  get PretApa(): FormControl{
    return this.TarifeForm.get("PretApa") as FormControl;
  }

  TransmitereData: Date = new Date();
  TarifeForm: FormGroup;
  isUpdate: boolean = false;
  isFormSubmitted: boolean = false;

  onEdit(): void {
    // If the form is disabled (in view mode), enable it (to edit mode)
    if (this.TarifeForm.disabled) {
      this.TarifeForm.enable();
      this.isNewSubmission = true; // set the flag here
    }
  }
  onSave(): void {
    if (this.TarifeForm.valid) {
      const token = localStorage.getItem('token');

      if (token) {
        const decodedToken = this.jwtHelperService.decodeToken(token);
        const iduser = decodedToken.sub;

        let tarife: TarifeDetail = this.TarifeForm.value;
        tarife.Id = this.tarife.Id;

        if (this.isUpdate) {
          // Check if tarife object has a valid IdTarife
          if (tarife.IdTarife) {
            // If data already exists, update the data
            this.tarifeService.updateTarife(tarife.IdTarife, tarife).subscribe(response => {
              console.log(response);
              console.log('Data updated successfully');
              // Update tarife with the updated one
              this.tarife = response;
              this.TarifeForm.disable();
            }, error => {
              console.error(error);
              // Handle error
            });

          } else {
            console.error('Tarife does not have a valid IdTarife');
          }
        } else {
          // Else, create new data
          this.tarifeService.createTarife(tarife).subscribe(response => {
            console.log(response);
            console.log('Data saved successfully');
            // Update tarife with the created one, which includes the new IdTarife
            this.tarife = response;
            this.isUpdate = true;
            this.TarifeForm.disable();
          }, error => {
            console.error(error);
            // Handle error
          });
        }
      }
    }
  }
}


