import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private url: string = "http://localhost:3000/products";

  constructor(private http: HttpClient) { }

  public GetAllProducts() : Observable<Array<any>> {
    return this.http.get<Array<any>>(`${this.url}`).pipe(
      res => res,
      error => error
    );
  }

  GetProductById(id: string) : Observable<any> {
    return this.http.get<any>(`${this.url}/${id}`).pipe(
      res => res,
      error => error
    );
  }
}
