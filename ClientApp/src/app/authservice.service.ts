import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})
export class AuthserviceService {

  constructor(private http: HttpClient) { }
  login(username: string, password: string) {
    return this.http.post<any>("http://localhost:5000/api/login", { username, password })
        .pipe(map(data => {
            if (data) {
              localStorage.setItem("jwt", data.token);
            }
            return data;
        }));
}

}
