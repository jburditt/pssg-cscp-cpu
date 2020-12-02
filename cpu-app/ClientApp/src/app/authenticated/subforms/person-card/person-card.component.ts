import { Component, OnInit, Input } from '@angular/core';
import { iPerson } from '../../../core/models/person.interface';
import { nameAssemble } from '../../../core/constants/name-assemble';

@Component({
  selector: 'app-person-card',
  templateUrl: './person-card.component.html',
  styleUrls: ['./person-card.component.css']
})
export class PersonCardComponent implements OnInit {
  @Input() person: iPerson;
  nameAssemble = nameAssemble;
  constructor() { }

  ngOnInit() {
  }

}
