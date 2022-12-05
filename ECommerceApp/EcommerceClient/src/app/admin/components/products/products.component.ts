import {AfterViewInit, Component, Injector, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {BaseComponent} from "../../../base/base.component";
import {MatPaginator} from "@angular/material/paginator";
import {MatTableDataSource} from "@angular/material/table";
import {ListProduct} from "../../../contracts/admin/products/list-product";
import {ProductService} from "../../../services/common/modules/product.service";
import {spinnerType} from "../../../base/spinnerType";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent extends BaseComponent implements OnInit, AfterViewInit {

  // @ts-ignore
  @ViewChild(MatPaginator) paginator: MatPaginator;

  // @ts-ignore
  @ViewChild('secondDialog', {static: true}) secondDialog: TemplateRef<any>;

  displayedColumns: string[] = ['name', 'stock', 'price', 'createDate', 'updateDate'];

  // @ts-ignore
  dataSource: MatTableDataSource<ListProduct> = null

  constructor(injector: Injector,
              private productService: ProductService) {
    super(injector);
  }

  ngAfterViewInit() {
    this.getAllProducts()
  }

  ngOnInit(): void {
  }

  getAllProducts() {
    this.showSpinner(spinnerType.BallAtom)
    this.productService.get(() => {
      this.hideSpinner(spinnerType.BallAtom);
    }, errorMessage => {
      this.hideSpinner(spinnerType.BallAtom);
      this.toastrService.error(errorMessage)
    }).then(r => {
      this.dataSource = new MatTableDataSource<ListProduct>(r)
      this.dataSource.paginator = this.paginator;
    })
  }

}
