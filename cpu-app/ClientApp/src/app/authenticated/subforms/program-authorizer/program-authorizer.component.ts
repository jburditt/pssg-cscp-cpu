import * as moment from 'moment';
import { Component, OnInit, Input, EventEmitter, Output, ViewChild } from '@angular/core';
import { Person } from '../../../core/models/person.class';
import { SignaturePad } from 'angular2-signaturepad/signature-pad';
import { StateService } from '../../../core/services/state.service';
import { iPerson } from '../../../core/models/person.interface';


export interface iSignature {
  signer: iPerson;
  signature?: any;
  signatureDate?: Date;
  termsConfirmation: boolean;
}
@Component({
  selector: 'app-program-authorizer',
  templateUrl: './program-authorizer.component.html',
  styleUrls: ['./program-authorizer.component.css']
})
export class ProgramAuthorizerComponent implements OnInit {
  @Input() signature: iSignature;
  @Input() isDisabled: boolean = false;
  @Input() formType: string;
  @Output() signatureChange = new EventEmitter<iSignature>();
  @ViewChild(SignaturePad) signaturePad: SignaturePad;

  public signatureImage: any;
  wasSigned: boolean = false;
  signatureData: string;
  signingDate: string;
  terms: [string, boolean][] = [];

  constructor(
    private stateService: StateService,
  ) { }

  ngOnInit() {
    this.terms = [
      [`I have reviewed the completed ${this.formType}; and that`, false],
      [`all of the information provided in this ${this.formType}, including all attachments, is accurate and correct to the best of my knowledge.`, false]
    ];
    if (!this.signature) {
      this.signature = {
        signer: new Person(this.stateService.currentUser.getValue()),
        termsConfirmation: false,
      }
    }
  }
  get state() {
    return this.terms.map(term => term[1]).reduce((prev: boolean, curr: boolean) => prev && curr);
  }
  onInput() {
    if (this.state) {
      this.signature.termsConfirmation = true;
      this.signatureChange.emit(this.signature);
    };
  }

  signaturePadOptions: Object = {
    'minWidth': 0.3,
    'maxWidth': 2.5,
    'canvasWidth': 600,
    'canvasHeight': 200,
  };
  clearSignature() {
    this.wasSigned = false;
    this.signatureData = null;
    this.signaturePad.clear();
    this.signature.signature = null;
    this.signature.signatureDate = null;
  }

  acceptSignature() {
    if (this.wasSigned) {
      this.signature.signature = this.signaturePad.toDataURL();
      this.signature.signatureDate = new Date();
    }
  }
  drawStart() {
    this.wasSigned = true;
  }
}
