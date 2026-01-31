import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, inject }             from '@angular/core';

/**
 * Class to represent confirm dialog model.
 *
 * It has been kept here to keep it as part of shared component.
 */
export class DialogConfirmModel {

  constructor(public title: string, public message: string) {
  }
}

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './dialog-confirm.component.html'
})
export class DialogConfirmComponent {
  title: string;
  message: string;

  private dialogRef = inject(MatDialogRef<DialogConfirmComponent>);
  private data = inject(MAT_DIALOG_DATA) as DialogConfirmModel;

  constructor() {
    // Update view with given values
    this.title = this.data.title;
    this.message = this.data.message;
  }

  onConfirm(): void {
    // Close the dialog, return true
    this.dialogRef.close(true);
  }

  onDismiss(): void {
    // Close the dialog, return false
    this.dialogRef.close(false);
  }
}

