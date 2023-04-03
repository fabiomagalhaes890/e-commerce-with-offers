import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CheckoutService } from '../services/checkout.service';
import { ShoppingCartService } from '../services/shopping-cart.service';
import { StorageService } from '../services/storage.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.scss']
})
export class ShoppingCartComponent implements OnInit {
  public shoppingCart: any = {};
  public checkout: any = {};
  
  constructor(
    private storage: StorageService,
    private shoppingCartService: ShoppingCartService,
    private checkoutService: CheckoutService,
    private router: Router
  ) { }

  BackShopping(){
    this.router.navigate(['products']);
  }

  Remove(item: any) {
    this.shoppingCartService.RemoveItem(item);
    this.ngOnInit();
  }

  AddItem(item: any) {
    this.shoppingCartService.AddItem(item);
    this.ngOnInit();
  }

  ReduceItem(item: any) {
    this.shoppingCartService.ReduceItem(item);
    this.ngOnInit();
  }

  CalculateCheckout() {
    if(this.shoppingCart.products) {
      this.checkoutService.Checkout(this.shoppingCart).subscribe({
        next: (data) => {
          this.shoppingCart = data;
          this.storage.saveShoppingCart(this.shoppingCart);
        },
        error: (err) => {
          console.log('error:', err);
        }
      });
    }
    else {
      this.router.navigate(['products']);
    }
  }

  ngOnInit(): void {
    this.shoppingCart = this.storage.getShoppingCart();
    this.CalculateCheckout();
  }
}
