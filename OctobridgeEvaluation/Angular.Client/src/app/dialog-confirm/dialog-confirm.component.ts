import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, Inject }             from '@angular/core';

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

  constructor(public dialogRef: MatDialogRef<DialogConfirmComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogConfirmModel) {
    // Update view with given values
    this.title = data.title;
    this.message = data.message;
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

