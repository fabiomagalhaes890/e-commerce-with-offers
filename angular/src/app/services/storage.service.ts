import { Injectable } from '@angular/core';

const USER_KEY = 'user_id';
const SC_KEY = 'shopping_cart';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor() { }

  clean(): void {
    window.sessionStorage.clear();
  }

  public saveUser(user: any): void {
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.setItem(USER_KEY, JSON.stringify(user));
  }

  public getUser(): any {
    const user = window.sessionStorage.getItem(USER_KEY);
    if (user) {
      return JSON.parse(user);
    }

    return {};
  }

  public saveShoppingCart(shoppingCart: any): void {
    window.sessionStorage.removeItem(SC_KEY);
    window.sessionStorage.setItem(SC_KEY, JSON.stringify(shoppingCart));
  }

  public getShoppingCart(): any {
    const token = window.sessionStorage.getItem(SC_KEY);
    if (token) {
      return JSON.parse(token);
    }

    return '';
  }
}
