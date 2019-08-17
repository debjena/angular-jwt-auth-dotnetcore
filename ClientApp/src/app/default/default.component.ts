import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from "@auth0/angular-jwt";
import { Router } from '@angular/router';

@Component({
  selector: 'app-default',
  templateUrl: './default.component.html',
  styleUrls: ['./default.component.css']
})
export class DefaultComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }
  isUserAuthenticated() {
    let token: string = localStorage.getItem("jwt");
    const helper = new JwtHelperService();
    const isExpired = helper.isTokenExpired(token);
    if (token && !isExpired) {
      return true;
    }
    else {
      return false;
    }
  }

  logOut() {
    localStorage.removeItem("jwt");
  }
}
