import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'cap-guidelines.dialog',
    templateUrl: 'cap-guidelines.dialog.html',
})
export class CAPGuidelinesDialog {
    constructor(public dialogRef: MatDialogRef<CAPGuidelinesDialog>,
        @Inject(MAT_DIALOG_DATA) public data: any) {
    }

    onOkayClick() {
        this.dialogRef.close();
    }

    close() {
        this.dialogRef.close();
    }
}
