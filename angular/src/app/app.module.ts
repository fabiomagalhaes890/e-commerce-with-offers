import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { ProductsComponent } from './products/products.component';
import { RouterModule, Routes } from '@angular/router';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';
import { ManageProductComponent } from './manage-product/manage-product.component';
import { AdminComponent } from './admin/admin.component';
import { CurrencyMaskModule } from 'ng2-currency-mask';

const routes: Routes = [
  { path: '', component: ProductsComponent },
  { path: 'admin', component: AdminComponent },
  { path: 'products', component: ProductsComponent },
  { path: 'checkout', component: ShoppingCartComponent },
  { path: 'manage', component: ManageProductComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    ProductsComponent,
    ShoppingCartComponent,
    ManageProductComponent,
    AdminComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes),
    CurrencyMaskModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
