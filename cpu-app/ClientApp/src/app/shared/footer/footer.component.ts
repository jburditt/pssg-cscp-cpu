import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {
  constructor(
    private router: Router
  ) { }

  ngOnInit() {
  }
  homeButton() {
    // this is done without a routerlink because you will want to route the user back to a place
    // that is appropriate for their role. So check their logged in state and etc before deciding which route they go to.
    this.router.navigate(['']);
  }
}
