import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from "@angular/router";
import { FormGroup,Validators, FormBuilder } from '@angular/forms';
import { AuthserviceService } from '../authservice.service'
import { first } from 'rxjs/operators'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  invalidLogin: boolean;
  submitted = false;
  loginForm : FormGroup;
  constructor(private router: Router,private authenticationService: AuthserviceService, private http: HttpClient,private formbuilder:FormBuilder) { }

  ngOnInit() {
    this.loginForm = this.formbuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
  });
  }
  get f() { return this.loginForm.controls; }
  login() {
    this.submitted = true;
    let u = this.loginForm.controls.username;
    console.log(u)
    let p = this.loginForm.controls.password;
    if (this.loginForm.invalid) {
      return;
    }
    let credentials = JSON.stringify(this.loginForm.value);
    // this.http.post("http://localhost:5000/api/login", credentials, {
    //   headers: new HttpHeaders({
    //     "Content-Type": "application/json"
    //   })
    // })
    this.authenticationService.login(this.f.username.value, this.f.password.value)
    .pipe(first())
    .subscribe(response => {
      let token = (<any>response).token;
      //localStorage.setItem("jwt", token);
      this.invalidLogin = false;
      this.router.navigate(["/"]);
    }, err => {
      this.invalidLogin = true;
    });
  }
}
