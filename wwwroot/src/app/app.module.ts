import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
//import { JwtModule } from "@auth0/angular-jwt";
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { DefaultComponent } from './default/default.component';
import { UsersComponent } from './users/users.component';
import { AuthGuard } from './auth.guard';
import { AuthserviceService } from './authservice.service';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DefaultComponent,
    UsersComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    // JwtModule.forRoot({
    //   config: {
    //     tokenGetter: function  tokenGetter() {
    //          return     localStorage.getItem('jwt');},
    //     whitelistedDomains: ['localhost:5000'],
    //     blacklistedRoutes: ['http://localhost:5000/api/login']
    //   }
    // }),
    RouterModule.forRoot([
      { path: '', component: DefaultComponent },
      { path: 'login', component: LoginComponent },
      { path: 'users', component: UsersComponent, canActivate: [AuthGuard] },
    ])
  ],
  providers: [AuthGuard,AuthserviceService],
  bootstrap: [AppComponent]
})
export class AppModule { }
