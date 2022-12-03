import {Component, EventEmitter, Injector, OnInit, Output, TemplateRef, ViewChild} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {BaseComponent} from "../../../../base/base.component";
import {ModalMode} from "../../../../base/modal-mode";
import {ProductService} from "../../../../services/common/modules/product.service";
import {CreateProduct} from "../../../../contracts/admin/products/create-product";
import {spinnerType} from "../../../../base/spinnerType";

@Component({
  selector: 'app-create-edit-product',
  templateUrl: './create-edit-product.component.html',
  styleUrls: ['./create-edit-product.component.scss']
})
export class CreateEditProductComponent extends BaseComponent implements OnInit {

  // @ts-ignore
  @ViewChild('secondDialog', {static: true}) secondDialog: TemplateRef<any>;
  @Output() onSave = new EventEmitter<any>();

  mode: ModalMode | undefined;

  isActive: boolean = false;

  input: CreateProduct = new CreateProduct();

  constructor(injector: Injector,
              private dialog: MatDialog,
              private productService: ProductService) {
    super(injector);
  }

  ngOnInit(): void {
  }

  show(mode: ModalMode, id?: string) {
    this.mode = mode
    this.input = new CreateProduct()
    if (this.mode == ModalMode.Edit || this.mode == ModalMode.Details) {

    }
    this.openDialog();
  }

  private openDialog() {
    this.dialog.open(this.secondDialog);
  }

  protected close() {
    this.dialog.closeAll();
  }

  save() {
    this.showSpinner(spinnerType.BallAtom)
    this.productService.create(this.input, () => {
      this.hideSpinner(spinnerType.BallAtom)
      this.toastrService.success("Success")

      this.close()
    }, errorMessage => {
      this.hideSpinner(spinnerType.BallAtom)
      this.toastrService.error(errorMessage)
    })
  }
}

