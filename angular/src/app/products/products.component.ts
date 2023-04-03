import { Component, OnInit } from '@angular/core';
import { ProductService } from '../services/product.service';
import { ShoppingCartService } from '../services/shopping-cart.service';
import { StorageService } from '../services/storage.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {
  public products: any = [];
  public totalItems: number = 0;
  
  constructor(
    private productService: ProductService,
    private userService: UserService,
    private shoppingCartService: ShoppingCartService,
    private storage: StorageService) { }

  public Buy(item: any) {
    this.shoppingCartService.PutShoppingCart(item);    
    this.totalItems = this.shoppingCartService.GetCountOfItems();
  }

  public GetAllProducts() {
    this.productService.GetAllProducts().subscribe({
      next: (data) => {
        this.products = data;
      },
      error: (err) => {
        console.log('error: ', err);
      }
    })
  }

  CreateShoppingCart() {
    let shoppingCart = {
      userId: this.storage.getUser().id,
      products: []
    };

    this.storage.saveShoppingCart(shoppingCart);
  }

  GetTotalItems() {
    this.totalItems = this.shoppingCartService.GetCountOfItems();
  }

  Clear() {
    this.storage.clean();
    this.ngOnInit();
  }

  ngOnInit(): void {
    this.userService.GetUserById(2).subscribe({
      next: (data) => {
        this.storage.saveUser(data);

        if(this.storage.getShoppingCart() === undefined 
          || this.storage.getShoppingCart() === null
          || this.storage.getShoppingCart() === '')
            this.CreateShoppingCart();
        
        this.GetTotalItems();
      },
      error: (err) => {
        console.log('error', err);
      }
    });

    this.GetAllProducts();
  }
}
