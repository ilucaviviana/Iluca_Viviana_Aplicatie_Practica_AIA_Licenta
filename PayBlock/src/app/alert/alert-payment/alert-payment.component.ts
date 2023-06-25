import { Component, OnInit } from '@angular/core';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-alert-payment',
  template: `
    <h1 mat-dialog-title style="text-align: center;">Plata a fost realizată cu succes!</h1>
    <div mat-dialog-content style="text-align: center;">
      <p>Veți fi redirecționat către pagina de facturi în câteva secunde.</p>
    </div>
    <div mat-dialog-actions style="center;">
    <button mat-button (click)="onClose()">OK</button>
    </div>
  `,
  styles: [
    `::ng-deep .mat-dialog-actions {flex-direction: row; justify-content: center;}`,
    `::ng-deep .mat-dialog-content {overflow: auto;}`
  ]
})
export class AlertPaymentComponent {

  constructor(public dialogRef: MatDialogRef<AlertPaymentComponent>) {}

  onClose(): void {
    this.dialogRef.close('ok');
  }
}

