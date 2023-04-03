import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ManageProductService } from '../services/manage-product.service';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-manage-product',
  templateUrl: './manage-product.component.html',
  styleUrls: ['./manage-product.component.scss']
})
export class ManageProductComponent implements OnInit {
  
  public currentProduct: any = {};
  public currentOffer: any = {};
  public offers: Array<any> = [];

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private router: Router,
    private manageService: ManageProductService
    ) {}

  BackToList() {
    this.router.navigate(['admin']);
  }

  SaveOffers() {
    this.currentOffer = { ...this.currentOffer, id: this.currentOffer.id ?? 0, productId: this.currentProduct.id };
    this.manageService.PersistOffer(this.currentOffer).subscribe({
      next: (data) => {
        this.ngOnInit();
      },
      error: (err) => {
        console.log('error', err);
      }
    });  
  }

  GetOffers() {
    this.manageService.GetOffer(this.currentProduct.id).subscribe({
      next: (data) => {
        this.offers = data;
      },
      error: (err) => {
        console.log('error', err);
      }
    })
  }

  SelectToEdit(item: any) {
    this.currentOffer = { ...item, price: '€'+ String(item.price) };
  }

  RemoveItem(item: any) {
    this.manageService.RemoveOffer(item.id).subscribe({
      next: (data) => {
      },
      error: (err) => {
        console.log('error', err);
      }
    });
    this.ngOnInit();
  }

  transform(event: any) {
    if(event.target.value != undefined || event.target.value != '') {
      var converted = new Intl.NumberFormat('de-DE',{style: 'currency', currency:'EUR'}).format(event.target.value);
      this.currentOffer.price = event.target.value;
      this.currentOffer.price = converted;
    }    
  }

  Check(event: any) {
    this.currentOffer = { ...this.currentOffer, status: event.target.checked}
  }

  ngOnInit(): void {
    let id = this.route.snapshot.paramMap.get('id');

    this.productService.GetProductById(id!).subscribe({
      next: (data) => {
        this.currentProduct = data;
        this.currentOffer = {};
        this.GetOffers();
      },
      error: (err) => {
        console.log('error', err);
      }
    })
  }
}
