import { Component, Input } from '@angular/core';
import { Address } from '../../../core/models/address.class';
import { TransmogrifierCAPApplication } from '../../../core/models/transmogrifier-cap-application.class';
import * as _ from 'lodash';
import { AddPersonDialog } from '../../dialogs/add-person/add-person.dialog';
import { MatDialog } from '@angular/material';
import { StateService } from '../../../core/services/state.service';
import { FormHelper } from '../../../core/form-helper';

@Component({
    selector: 'app-applicant-information',
    templateUrl: './applicant-information.component.html',
    styleUrls: ['./applicant-information.component.scss']
})
export class ApplicantInformationComponent {
    @Input() trans: TransmogrifierCAPApplication;
    @Input() isCompleted: boolean;

    public formHelper = new FormHelper();

    constructor(public dialog: MatDialog,
    ) { }
}