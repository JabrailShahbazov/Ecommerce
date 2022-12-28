import {Component, Inject, Injector, OnInit, Output} from '@angular/core';
import {BaseDialog} from "../base/base-dialog";
import {MAT_DIALOG_DATA} from "@angular/material/dialog";
import {FileUploadDialogState} from "../file-upload-dialog/file-upload-dialog.component";
import {FileUploadOptions} from "../../services/common/file-upload/file-upload.component";
import {ProductService} from "../../services/common/modules/product.service";
import {ListProductImage} from "../../contracts/admin/products/list-product-image";

@Component({
  selector: 'app-select-product-image-dialog',
  templateUrl: './select-product-image-dialog.component.html',
  styleUrls: ['./select-product-image-dialog.component.scss']
})
export class SelectProductImageDialogComponent extends BaseDialog<SelectProductImageDialogComponent> implements OnInit {


  @Output() options: Partial<FileUploadOptions> = {
    accept: '.png, .jpg, .jpeg, .gif',
    action: 'upload',
    controller: 'product',
    explanation: 'Drop or select files',
    queryString: `id=${this.data}`
  }

  constructor(injector: Injector,
              @Inject(MAT_DIALOG_DATA) public data: SelectProductImageState | string,
              private readonly productService: ProductService) {
    super(injector);
  }

  images: ListProductImage[] = []

  async ngOnInit() {
    this.images = await this.productService.getImages(this.data as string)
  }

}

export enum SelectProductImageState {
  Close
}
