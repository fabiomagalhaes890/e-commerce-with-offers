import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  private url: string = "http://localhost:44300/checkout";

  constructor(private http: HttpClient) { }

  public Checkout(shoppingCart: any) : Observable<any> {

    return this.http.post<any>(`${this.url}`, shoppingCart).pipe(
      res => res,
      error => error
    );
  }
}
