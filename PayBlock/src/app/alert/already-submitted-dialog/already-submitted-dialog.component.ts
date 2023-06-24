import { Component } from '@angular/core';

@Component({
  selector: 'app-already-submitted-dialog',
  template: `
    <h1 mat-dialog-title>Alertă!</h1>
    <div mat-dialog-content>Ați transmis deja datele pentru această lună!</div>
    <div mat-dialog-actions>
      <button mat-button mat-dialog-close>Okay</button>
    </div>
  `,
})
export class AlreadySubmittedDialogComponent {}
