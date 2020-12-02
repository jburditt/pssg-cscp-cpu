import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'appendix-a.dialog',
  templateUrl: 'appendix-a.dialog.html',
})
export class AppendixADialog {
  constructor(public dialogRef: MatDialogRef<AppendixADialog>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
  }

  onOkayClick() {
    this.dialogRef.close();
  }

  close() {
    this.dialogRef.close();
  }
}
