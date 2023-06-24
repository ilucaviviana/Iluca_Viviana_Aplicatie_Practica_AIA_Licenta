import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { PlataDetailService } from '../shared/plata-detail.service';
import { Router } from '@angular/router';
import { jsPDF } from 'jspdf';
import html2canvas from 'html2canvas';
import autoTable from 'jspdf-autotable';


@Component({
  selector: 'app-plata-page',
  templateUrl: './plata-page.component.html',
  styleUrls: ['./plata-page.component.css']
})
export class PlataPageComponent implements OnInit {

  constructor(private http: HttpClient, private plataService: PlataDetailService, private router: Router) { }
  data: any;
  userLocation: any;
  ngOnInit() {
    this.getTarife();
    this.plataService.getData().subscribe(data => {
      this.data = data;
    });

    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken = this.jwtHelperService.decodeToken(token);
      const userId = decodedToken.sub;
      this.getFacturi(userId);
      this.getLocation(userId);  // fetch the location
    }
  }

  tarife: any = [];
  latestTarif: any = {};
  jwtHelperService = new JwtHelperService();
  invoices: any[] = [];

  getTarife() {
    this.http.get('https://localhost:44316/api/Tarife').subscribe(data => {
      this.tarife = data;
      this.latestTarif = this.tarife[this.tarife.length - 1];  // store the latest tarif
    }, error => console.error(error));
  }

  getFacturi(userId: number) {
    this.http.get<any[]>(`https://localhost:44316/api/Factura/user/${userId}`).subscribe(data => {
      console.log(data);
       this.invoices = data;
    }, error => console.error(error));
  }
  payInvoice(invoice: any) {
    this.router.navigate(['stripe'], { state: { invoice: invoice } });
  }

  getLocation(userId: number) {
    this.http.get<any>(`https://localhost:44316/api/Locatii/${userId}`).subscribe(data => {
      console.log(data);
      this.userLocation = data;
    }, error => console.error(error));
  }

// Generate PDF
generatePDF(invoice: any) {
  const doc = new jsPDF();

  // Add title
  doc.setFontSize(20);
  doc.text('Detalii factura', 15, 15);

  doc.setFontSize(12);
  doc.text(`Locatie: `, 15, 25);
  doc.text(`Judet: ${this.userLocation.Judet}`, 15, 30);
  doc.text(`Oras: ${this.userLocation.Oras}`, 15, 35);
  doc.text(`Strada: ${this.userLocation.Strada}`, 15, 40);
  doc.text(`Bloc: ${this.userLocation.Bloc}`, 15, 45);
  doc.text(`Apartament: ${this.userLocation.Apartament}`, 15, 50);

  const date = new Date(invoice.TransmitereData);
  const formattedDate = `${date.getFullYear()}-${String(date.getMonth() + 1).padStart(2, '0')}-${String(date.getDate()).padStart(2, '0')}`;

  const body = [
    [
      `${invoice.TotalApa} RON`,
      `${invoice.TotalRetim} RON`,
      `${invoice.TotalAdmin} RON`,
      `${invoice.TotalCuratenie} RON`,
      formattedDate,
      `${invoice.TotalApa + invoice.TotalRetim + invoice.TotalAdmin + invoice.TotalCuratenie} RON`,
    ]
  ];

  autoTable(doc, {
    startY: 55, // You may need to adjust this value to position the table below the title
    head: [['Apa', 'Retim', 'Administrator', 'Curatenie', 'Data', 'Total factura']],
    body: body
  });

  doc.save('Factura.pdf');
}





}
