import {Component, Inject, Injector} from '@angular/core';
import {BaseDialog} from "../base/base-dialog";
import {MAT_DIALOG_DATA} from "@angular/material/dialog";
import {HttpClientService} from "../../services/common/http-client.service";

@Component({
  selector: 'app-file-upload-dialog',
  templateUrl: './file-upload-dialog.component.html',
  styleUrls: ['./file-upload-dialog.component.scss']
})
export class FileUploadDialogComponent extends BaseDialog<FileUploadDialogComponent> {

  constructor(injector: Injector,
              @Inject(MAT_DIALOG_DATA) public data : FileUploadDialogState,
              private httpClientService: HttpClientService) {
    super(injector);
  }

}
export  enum FileUploadDialogState{
  Yes,No
}
