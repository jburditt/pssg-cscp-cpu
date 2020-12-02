import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { uuidv4 } from '../../../core/constants/uuidv4';
import { iHours } from '../../../core/models/hours.interface';
import { TIME } from '../../../core/constants/regex.constants';
import { FormHelper } from '../../../core/form-helper';

@Component({
  selector: 'app-hours',
  templateUrl: './hours.component.html',
  styleUrls: ['./hours.component.css']
})
export class HoursComponent implements OnInit {
  @Input() hours: iHours;
  @Input() isDisabled: boolean = false;
  @Output() hoursChange = new EventEmitter<iHours>();
  @Input() title: string = 'Hours';
  timeRegex: RegExp = TIME;
  uuid: string;
  public formHelper = new FormHelper();
  constructor() { }

  ngOnInit() {
    this.uuid = uuidv4();
  }
}
