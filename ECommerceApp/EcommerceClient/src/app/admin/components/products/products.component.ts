import {AfterViewInit, Component, Injector, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {BaseComponent} from "../../../base/base.component";
import {MatPaginator} from "@angular/material/paginator";
import {MatTableDataSource} from "@angular/material/table";
import {ProductListDto} from "../../../contracts/admin/products/list-product";
import {ProductService} from "../../../services/common/modules/product.service";
import {spinnerType} from "../../../base/spinnerType";
import {PaginationDto} from "../../../contracts/common/pagination-dto";


declare var $:any
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

  displayedColumns: string[] = ['name', 'stock', 'price', 'createDate', 'updateDate', 'delete','update'];

  // @ts-ignore
  dataSource: MatTableDataSource<ListProduct> = null

  constructor(injector: Injector,
              private productService: ProductService) {
    super(injector);
  }

  async ngAfterViewInit() {
    await this.getAllProducts()
  }

  ngOnInit(): void {
  }

  async getAllProducts() {
    this.showSpinner(spinnerType.BallAtom)

    let page = new PaginationDto

    page.page = this.paginator.pageIndex == null ? 0 : this.paginator.pageIndex
    page.size = this.paginator.pageSize == null ? 5 : this.paginator.pageSize
    const allProducts = await this.productService.get(page, () => {
      this.hideSpinner(spinnerType.BallAtom);
    }, errorMessage => {
      this.hideSpinner(spinnerType.BallAtom);
      this.toastrService.error(errorMessage)
    })

    let Plist: ProductListDto = new ProductListDto()
    let data: any
    if (allProducts?.items) {
      data = Object.assign(allProducts?.items, Plist)
    }

    this.dataSource = new MatTableDataSource<ProductListDto>(data)
    this.paginator.length = allProducts?.totalCount
  }

  async pageChanged() {
    await this.getAllProducts()
  }

}
