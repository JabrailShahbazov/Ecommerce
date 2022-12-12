import {Component, EventEmitter, Injector, OnInit, Output, TemplateRef, ViewChild} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {BaseComponent} from "../../../../base/base.component";
import {ModalMode} from "../../../../base/modal-mode";
import {ProductService} from "../../../../services/common/modules/product.service";
import {CreateProduct} from "../../../../contracts/admin/products/create-product";
import {spinnerType} from "../../../../base/spinnerType";
import {UpdateProduct} from "../../../../contracts/admin/products/update-product";
import {FileUploadOptions} from "../../../../services/common/file-upload/file-upload.component";

@Component({
  selector: 'app-create-edit-product',
  templateUrl: './create-edit-product.component.html',
  styleUrls: ['./create-edit-product.component.scss']
})
export class CreateEditProductComponent extends BaseComponent implements OnInit {

  // @ts-ignore
  @ViewChild('secondDialog', {static: true}) secondDialog: TemplateRef<any>;

  @Output() onSave = new EventEmitter<any>();

  @Output() fileUploadOptions: Partial<FileUploadOptions> = {
    controller: 'product',
    action: 'upload',
    explanation: 'Drop and Add Images',
    accept:'.png, .jpg, .jpeg, .json'
  }

  mode: ModalMode | undefined;

  isActive: boolean = false;

  input: CreateProduct = new CreateProduct();

  private id: number | undefined;

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
      if (id) {
        this.getById(id);
      } else {
        this.toastrService.error('Id is not')
        this.close()
      }
    } else {
      this.openDialog();
    }
  }

  private getById(id: string) {
    this.showSpinner(spinnerType.BallAtom)
    this.productService.getById(id, () => {
      this.hideSpinner(spinnerType.BallAtom);
    }, errorMessage => {
      this.hideSpinner(spinnerType.BallAtom);
      this.toastrService.error(errorMessage)
      this.close()
    }).then(r => {
      this.input.name = r?.name
      this.input.stock = r?.stock
      this.input.price = r?.price
      this.id = r?.id
      this.openDialog();
    })
  }

  private openDialog() {
    this.dialog.open(this.secondDialog);
  }

  protected close() {
    this.dialog.closeAll();
  }

  save() {
    this.showSpinner(spinnerType.BallAtom)
    if (this.mode == ModalMode.Create) {
      this.create()
    } else {
      this.update()
    }
  }

  private update() {
    let update = new UpdateProduct()

    update = Object.assign(this.input, update)
    update.id = this.id
    this.productService.update(update, () => {
      this.hideSpinner(spinnerType.BallAtom)
      this.toastrService.success("Success")
      this.close()
      this.onSave.emit()
    }, errorMessage => {
      this.hideSpinner(spinnerType.BallAtom)
      this.toastrService.error(errorMessage)
    })
  }

  private create() {
    this.productService.create(this.input, () => {
      this.hideSpinner(spinnerType.BallAtom)
      this.toastrService.success("Success")
      this.close()
      this.onSave.emit()
    }, errorMessage => {
      this.hideSpinner(spinnerType.BallAtom)
      this.toastrService.error(errorMessage)
    })
  }
}

