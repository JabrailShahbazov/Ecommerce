import {Component, Inject, Injector, OnInit, Output} from '@angular/core';
import {BaseDialog} from "../base/base-dialog";
import {MAT_DIALOG_DATA} from "@angular/material/dialog";
import {FileUploadOptions} from "../../services/common/file-upload/file-upload.component";
import {ProductService} from "../../services/common/modules/product.service";
import {ListProductImage} from "../../contracts/admin/products/list-product-image";
import {spinnerType} from "../../base/spinnerType";
import {DialogService} from "../../services/common/dialog.service";
import {FileUploadDialogComponent, FileUploadDialogState} from "../file-upload-dialog/file-upload-dialog.component";

declare var $: any

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
              private readonly productService: ProductService,
              private dialogService: DialogService) {
    super(injector);
  }

  images: ListProductImage[] = []

  async ngOnInit() {
    this.images = await this.productService.getImages(this.data as string)
  }

  async deleteImage(imageId: any, event: any) {

    this.dialogService.openDialog({
      componentType: FileUploadDialogComponent,
      data: FileUploadDialogState.Yes,
      afterClosed: () => {
        this.showSpinner(spinnerType.BallAtom)
        this.productService.deleteImage(this.data as string, imageId, () => {
          this.hideSpinner(spinnerType.BallAtom)
          this.toastrService.success("Deleted")

          var card = $(event.srcElement).parent().parent().parent()

          card.fadeOut(500)
        })
      }
    })
  }
}

export enum SelectProductImageState {
  Close
}
