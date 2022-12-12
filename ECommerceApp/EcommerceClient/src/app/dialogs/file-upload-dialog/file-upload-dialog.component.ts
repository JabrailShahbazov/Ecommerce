import {Component, Injector, Input, OnInit} from '@angular/core';
import {BaseDialog} from "../base/base-dialog";
import {MatDialogRef} from "@angular/material/dialog";
import {HttpClientService} from "../../services/common/http-client.service";
import {NgxFileDropEntry} from "ngx-file-drop";
import {FileUploadOptions} from "../../services/common/file-upload/file-upload.component";

@Component({
  selector: 'app-file-upload-dialog',
  templateUrl: './file-upload-dialog.component.html',
  styleUrls: ['./file-upload-dialog.component.scss']
})
export class FileUploadDialogComponent extends BaseDialog<FileUploadDialogComponent> {

  constructor(injector: Injector,
              private httpClientService: HttpClientService) {
    super(injector);
  }

  public files: NgxFileDropEntry[];

  @Input() options: Partial<FileUploadOptions>

}
