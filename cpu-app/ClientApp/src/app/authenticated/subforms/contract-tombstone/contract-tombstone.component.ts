import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-contract-tombstone',
  templateUrl: './contract-tombstone.component.html',
  styleUrls: ['./contract-tombstone.component.scss']
})
export class ContractTombstoneComponent implements OnInit {
  @Input() contractNumber: string;
  @Input() organizationName: string;

  constructor() { }

  ngOnInit() {
  }

}
