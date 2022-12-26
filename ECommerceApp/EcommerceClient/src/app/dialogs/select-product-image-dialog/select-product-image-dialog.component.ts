import {Component, Inject, Injector, OnInit, Output} from '@angular/core';
import {BaseDialog} from "../base/base-dialog";
import {MAT_DIALOG_DATA} from "@angular/material/dialog";
import {FileUploadDialogState} from "../file-upload-dialog/file-upload-dialog.component";
import {FileUploadOptions} from "../../services/common/file-upload/file-upload.component";

@Component({
  selector: 'app-select-product-image-dialog',
  templateUrl: './select-product-image-dialog.component.html',
  styleUrls: ['./select-product-image-dialog.component.scss']
})
export class SelectProductImageDialogComponent extends BaseDialog<SelectProductImageDialogComponent> {


  @Output() options: Partial<FileUploadOptions> = {
    accept: '.png, .jpg, .jpeg, .gif',
    action: 'upload',
    controller: 'product',
    explanation: 'Drop or select files',
    queryString: `id=${this.data}`
  }

  constructor(injector: Injector,
              @Inject(MAT_DIALOG_DATA) public data: SelectProductImageState | string) {
    super(injector);
  }

}

export enum SelectProductImageState {
  Close
}
