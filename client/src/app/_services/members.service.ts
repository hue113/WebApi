import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Member } from '../_models/member';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];

  constructor(private http: HttpClient) {}

  getMembers() {
    return this.http.get<Member[]>(
      this.baseUrl + 'users'
      // this.getHttpOptions()  // don't need this anymore, because jwtInterceptor will do this part
    );
  }

  getMember(username: string) {
    return this.http.get<Member>(
      this.baseUrl + 'users/' + username
      // this.getHttpOptions()  // don't need this anymore, because jwtInterceptor will do this part
    );
  }

  // don't need this method anymore, because jwtInterceptor will do this part
  // getHttpOptions() {
  //   const userString = localStorage.getItem('user');
  //   if (!userString) return;
  //   const user = JSON.parse(userString);
  //   return {
  //     headers: new HttpHeaders({
  //       Authorization: 'Bearer ' + user.token,
  //     }),
  //   };
  // }
}
