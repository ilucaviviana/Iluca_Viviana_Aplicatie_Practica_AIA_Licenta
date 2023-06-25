import { Component } from '@angular/core';


@Component({
  selector: 'app-alert-dialog',
  template: `
    <h1 mat-dialog-title>Atenție!</h1>
    <div mat-dialog-content>
      <p>Aveți deja o adresă selectată. Nu mai puteți adăuga alta!</p>
    </div>
    <div mat-dialog-actions>
      <button mat-button mat-dialog-close>OK</button>
    </div>
  `,
})
export class AlertDialogComponent {

}
