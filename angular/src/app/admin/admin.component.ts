import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  public products: any = [];
  public totalItems: number = 0;

  constructor(
    private productService: ProductService,
    private router: Router
    ) {}

  public ManageProduct(item: any) {
    this.router.navigate(['manage', { id: item.id }]);
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

  ngOnInit(): void {
    this.GetAllProducts();
  }
}
