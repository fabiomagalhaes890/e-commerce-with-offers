import { Injectable } from '@angular/core';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {

  private totalItems: number = 0;

  constructor(
    private storage: StorageService
  ) { }

  public PutShoppingCart(item: any) : void {

    let shoppingCart = this.storage.getShoppingCart();

    var product = {
      product: item,
      totalPrice: item.price,
      count: 1
    };

    if(shoppingCart.products) {
      var found = shoppingCart.products.filter((product: any) => product.product.id === item.id)[0];

      if(found != null || found != undefined) {
        let index = shoppingCart.products.indexOf(found);
        shoppingCart.products[index].count++;
        shoppingCart.products[index].totalPrice = item.price * shoppingCart.products[index].count;
        
        this.GetCountOfItems();
      }
      else {
        shoppingCart.products.push(product);
      }      
    }
    else {
      shoppingCart.products = new Array<any>();
      shoppingCart.products.push(product);
    }

    this.storage.saveShoppingCart(shoppingCart);
  }

  GetCountOfItems() : number {
    let shoppingCart = this.storage.getShoppingCart();
    
    if(shoppingCart.products) {
      this.totalItems = shoppingCart.products.reduce((total: any, product: any) => {
        return total + product.count
      },0);
    }

    return this.totalItems;
  }
  
  RemoveItem(item: any) {
    let shoppingCart = this.storage.getShoppingCart();
    
    var found = shoppingCart.products.filter((productItem: any) => productItem.product.id == item.product.id)[0];
    var index = shoppingCart.products.indexOf(found);
    shoppingCart.products.splice(index , 1);

    this.storage.saveShoppingCart(shoppingCart);
  }

  AddItem(item: any) {
    let shoppingCart = this.storage.getShoppingCart();
    
    var found = shoppingCart.products.filter((productItem: any) => productItem.product.id == item.product.id)[0];
    var index = shoppingCart.products.indexOf(found);
    shoppingCart.products[index].count++;
    shoppingCart.products[index].totalPrice = item.product.price * shoppingCart.products[index].count;

    this.storage.saveShoppingCart(shoppingCart);
  }

  ReduceItem(item: any) {
    let shoppingCart = this.storage.getShoppingCart();
    
    var found = shoppingCart.products.filter((productItem: any) => productItem.product.id == item.product.id)[0];
    var index = shoppingCart.products.indexOf(found);

    if(shoppingCart.products[index].count == 1)
      this.RemoveItem(shoppingCart.products[index]);
    else {
      shoppingCart.products[index].count--;
      shoppingCart.products[index].totalPrice = item.product.price * shoppingCart.products[index].count;
  
      this.storage.saveShoppingCart(shoppingCart);
    }
  }
}
  