import { Component, Inject, Input } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { iPerson } from '../../../core/models/person.interface';
import { iProgramApplication } from '../../../core/models/program-application.interface';
import { nameAssemble } from '../../../core/constants/name-assemble';
import * as _ from 'lodash'
import { Person } from '../../../core/models/person.class';
import { FormHelper } from '../../../core/form-helper';
import { NotificationQueueService } from '../../../core/services/notification-queue.service';
import { convertPersonnelToDynamics } from '../../../core/models/converters/personnel-to-dynamics';
import { StateService } from '../../../core/services/state.service';
import { PersonService } from '../../../core/services/person.service';
import { Address } from '../../../core/models/address.class';
import { iAddress } from '../../../core/models/address.interface';

@Component({
    selector: 'add-person.dialog',
    templateUrl: 'add-person.dialog.html',
    styleUrls: ['./add-person.dialog.scss']
})
export class AddPersonDialog {
    agencyAddress: iAddress;
    person: iPerson;

    public nameAssemble = nameAssemble;
    public formHelper = new FormHelper();
    saving: boolean = false;

    constructor(public dialogRef: MatDialogRef<AddPersonDialog>,
        @Inject(MAT_DIALOG_DATA) public data: any,
        private stateService: StateService,
        private personService: PersonService,
        private notificationQueueService: NotificationQueueService) {
        this.agencyAddress = data.agencyAddress;
        this.person = new Person();
    }

    setAddressSameAsAgency(person: iPerson) {
        let addressCopy = _.cloneDeep(this.agencyAddress)
        person.address = addressCopy;
    }
    clearAddress(person: iPerson) {
        person.address = new Address();
    }

    save() {
        try {
            if (!this.formHelper.isDialogValid(this.notificationQueueService)) {
                return;
            }
            if (!this.person.employmentStatus || this.person.employmentStatus === "null") {
                this.notificationQueueService.addNotification('Employment status is required.', 'warning');
                return;
            }
            this.saving = true;

            let thisPerson = new Person(this.person);
            if (thisPerson.hasRequiredFields()) {
                const userId = this.stateService.main.getValue().userId;
                const organizationId = this.stateService.main.getValue().organizationId;
                const post = convertPersonnelToDynamics(userId, organizationId, [this.person]);
                this.personService.setPersons(post).subscribe(
                    (r) => {
                        if (r.IsSuccess) {
                            this.saving = false;
                            this.notificationQueueService.addNotification(`Information is saved for ${nameAssemble(this.person.firstName, this.person.middleName, this.person.lastName)}`, 'success');
                            this.dialogRef.close(true);
                        }
                        else {
                            this.notificationQueueService.addNotification("There was a problem saving this person. If this problem is persisting please contact your ministry representative.", 'danger');
                            this.saving = false;
                        }
                    },
                    err => {
                        this.notificationQueueService.addNotification(err, 'danger');
                        this.saving = false;
                    }
                );
            } else {
                this.saving = false;
                this.notificationQueueService.addNotification('Please fill in required fields.', 'warning');
            }
        }
        catch (err) {
            console.log(err);
            this.notificationQueueService.addNotification('The agency staff could not be saved. If this problem is persisting please contact your ministry representative.', 'danger');
            this.saving = false;
        }
    }

    close() {
        this.dialogRef.close();
    }
}
