import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'program-egilibility.dialog',
    templateUrl: 'program-egilibility.dialog.html',
})
export class ProgramEgilibilityDialog {
    constructor(public dialogRef: MatDialogRef<ProgramEgilibilityDialog>,
        @Inject(MAT_DIALOG_DATA) public data: any) {
    }

    onOkayClick() {
        this.dialogRef.close();
    }

    close() {
        this.dialogRef.close();
    }
}
