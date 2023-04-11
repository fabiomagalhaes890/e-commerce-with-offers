import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ManageProductService {
  private url: string = "http://localhost:44300/offer";

  constructor(private http: HttpClient) { }

  PersistOffer(currentOffer: any)  : Observable<Array<any>> {
    let offer = currentOffer;

    if(offer.id == 0) {
      return this.http.post<any>(`${this.url}`, offer).pipe(
        res => res,
        error => error
      );
    }
    else {
      return this.http.put<any>(`${this.url}`, offer).pipe(
        res => res,
        error => error
      );
    }
    
  }

  GetOffer(id: string) : Observable<any> {
    return this.http.get<any>(`${this.url}/product-id/${id}`).pipe(
      res => res,
      error => error
    );
  }

  RemoveOffer(id: number) : Observable<any> {
    return this.http.delete<any>(`${this.url}/id/${id}`).pipe(
      res => res,
      error => error
    );
  }
}
