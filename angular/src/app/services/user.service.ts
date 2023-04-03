import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private url: string = "http://localhost:3000/users";

  constructor(private http: HttpClient) { }

  public GetUserById(id: number) : Observable<any> {
    return this.http.get<any>(`${this.url}/${id}`).pipe(
      res => res,
      error => error
    );
  }
}
