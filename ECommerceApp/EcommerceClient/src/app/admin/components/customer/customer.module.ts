import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CustomerComponent} from './customer.component';
import {RouterModule, Routes} from "@angular/router";
import {LayoutComponent} from "../../layout/layout.component";

const routes: Routes = [
  {path: "", component: CustomerComponent}
];

@NgModule({
  declarations: [
    CustomerComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class CustomerModule {
}
