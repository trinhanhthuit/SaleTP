import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from "@angular/router";
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(
    private router: Router,
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    };
    this.router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
        this.router.navigated = false;
      }
    });
  }
  title = 'UIBackEnd';
  isLogin = false;

}


