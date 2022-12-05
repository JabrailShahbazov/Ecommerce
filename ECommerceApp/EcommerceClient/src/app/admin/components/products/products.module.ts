import {NgModule} from '@angular/core';
import {CommonModule, DatePipe} from '@angular/common';
import {ProductsComponent} from './products.component';
import {RouterModule, Routes} from "@angular/router";
import {MatButtonModule} from "@angular/material/button";
import { CreateEditProductComponent } from './create-edit-product/create-edit-product.component';
import {MatDialogModule} from "@angular/material/dialog";
import {MatFormFieldModule} from "@angular/material/form-field";
import {FormsModule} from "@angular/forms";
import {MatInputModule} from "@angular/material/input";
import {MatCheckboxModule} from "@angular/material/checkbox";
import {MatSlideToggleModule} from "@angular/material/slide-toggle";
import {MatIconModule} from "@angular/material/icon";
import {MatGridListModule} from "@angular/material/grid-list";
import {MatPaginatorModule} from "@angular/material/paginator";
import {MatTableModule} from "@angular/material/table";

const routes: Routes = [
  {path: "", component: ProductsComponent}
]

@NgModule({
  declarations: [
    ProductsComponent,
    CreateEditProductComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatButtonModule,
    MatDialogModule,
    MatCheckboxModule,
    MatSlideToggleModule,
    MatIconModule,
    MatFormFieldModule,
    MatGridListModule,
    MatInputModule,
    FormsModule,
    MatPaginatorModule,
    MatTableModule
  ]
})
export class ProductsModule {
}
