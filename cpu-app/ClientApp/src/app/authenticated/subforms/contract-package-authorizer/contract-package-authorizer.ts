import { Component, OnInit, Input, EventEmitter, Output, ViewChild } from '@angular/core';
import { Person } from '../../../core/models/person.class';
import { SignaturePad } from 'angular2-signaturepad/signature-pad';
import { StateService } from '../../../core/services/state.service';
import { iPerson } from '../../../core/models/person.interface';
import * as _ from 'lodash';
import { iSignature } from '../program-authorizer/program-authorizer.component';


@Component({
  selector: 'app-contract-package-authorizer',
  templateUrl: './contract-package-authorizer.html',
  styleUrls: ['./contract-package-authorizer.scss']
})
export class ContractPackageAuthorizerComponent implements OnInit {
  @Input() signature: iSignature;
  @Input() isDisabled: boolean = false;
  @Output() signatureChange = new EventEmitter<iSignature>();
  @ViewChild(SignaturePad) signaturePad: SignaturePad;

  public signatureImage: any;
  wasSigned: boolean = false;
  signatureData: string;
  signingDate: string;
  terms: [string, boolean][] = [
    ['I understand that the Application Program for Community Safety and Crime Prevention Branch may notify the above authorities that I have submitted an application', false],
    ['I have read and understood the above information', false]
  ]
  constructor(
    private stateService: StateService,
  ) { }

  ngOnInit() {
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
