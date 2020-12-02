import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-tool-tip',
  templateUrl: './tool-tip.component.html'
})
export class ToolTipTriggerComponent implements OnInit {
  @Input() trigger = '';
  @Input() iconColor = '';

  styles: any = {};

  constructor() { }

  ngOnInit() {
    this.styles = { color: this.iconColor };
  }

}
