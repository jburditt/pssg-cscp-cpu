import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { TransmogrifierProgramApplication } from '../../../core/models/transmogrifier-program-application.class';
import { iProgramApplication } from '../../../core/models/program-application.interface';

@Component({
  selector: 'app-review-application',
  templateUrl: './review-application.component.html',
  styleUrls: ['./review-application.component.css']
})
export class ReviewApplicationComponent implements OnInit {
  @Input() trans: TransmogrifierProgramApplication;
  @Input() currentTab: string;
  @Output() tabChange = new EventEmitter<string>();

  tabs: string[] = ['Application Information'];
  tabIndex: number = 0;

  constructor() { }

  ngOnInit() {
    this.trans.programApplications.forEach((p: iProgramApplication) => { this.tabs.push(p.name) });
  }
  setCurrentTab(tab: string) {
    this.currentTab = tab;
    this.tabIndex = this.tabs.indexOf(this.currentTab);
    this.tabChange.emit(this.currentTab);
  }
  nextPage() {
    const index = this.tabs.indexOf(this.currentTab);
    if (!(index >= this.tabs.length - 1)) {
      this.currentTab = this.tabs[index + 1];
      this.tabIndex = index + 1;
      this.tabChange.emit(this.currentTab);
      window.scrollTo(0, 0);
    }
  }
  prevPage() {
    const index = this.tabs.indexOf(this.currentTab);
    if (index > 0) {
      this.currentTab = this.tabs[index - 1];
      this.tabIndex = index - 1;
      this.tabChange.emit(this.currentTab);
      window.scrollTo(0, 0);
    }
  }
}
