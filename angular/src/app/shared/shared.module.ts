import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartButtonComponent } from './cart-button/cart-button.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ListComponent } from './list/list.component';



@NgModule({
  declarations: [
    CartButtonComponent,
    NavMenuComponent,
    ListComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    NavMenuComponent,
    ListComponent,
    CartButtonComponent
  ]
})
export class SharedModule { }
