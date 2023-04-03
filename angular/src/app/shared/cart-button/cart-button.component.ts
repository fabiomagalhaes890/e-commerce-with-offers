import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart-button',
  templateUrl: './cart-button.component.html',
  styleUrls: ['./cart-button.component.scss']
})
export class CartButtonComponent {

  @Input() public total: number = 0;

  constructor(private router: Router){}

  RedirectToShopCart() {
    this.router.navigate(['checkout']);
  }
}
