import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-cover-letter',
  templateUrl: './cover-letter.component.html',
  styleUrls: ['./cover-letter.component.css']
})
export class CoverLetterComponent implements OnInit {

  constructor(private route: ActivatedRoute) { }
  taskId: string;
  ngOnInit() {
    this.route.params.subscribe((p) => this.taskId = p['taskId']);
  }

}
