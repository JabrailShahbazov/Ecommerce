import {Component, Injector, Input, OnInit} from '@angular/core';
import {FileSystemFileEntry, NgxFileDropEntry} from "ngx-file-drop";
import {HttpClientService} from "../http-client.service";
import {HttpErrorResponse, HttpHeaders} from "@angular/common/http";
import {BaseComponent} from "../../../base/base.component";

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent extends BaseComponent implements OnInit {

  constructor(injector: Injector,
              private httpClientService: HttpClientService) {
    super(injector)
  }

  ngOnInit(): void {
  }

  public files: NgxFileDropEntry[] = [];

  @Input() options: Partial<FileUploadOptions> = new FileUploadOptions()

  public selectedFiles(files: NgxFileDropEntry[]) {
    this.files = files;
    for (const droppedFile of files) {
      const fileData: FormData = new FormData()
      for (let file of files) {
        (file.fileEntry as FileSystemFileEntry).file((_file: File) => {
          fileData.append(_file.name, _file, file.relativePath);


        });
      }
debugger
      this.httpClientService.post({
        controller: this.options.controller,
        action: this.options.action,
        headers: new HttpHeaders({'responseType': 'blob'})
      }, fileData).subscribe(data => {
        this.toastrService.success("Your Files Added")
      }, (errorResponse: HttpErrorResponse) => {
        this.toastrService.error(errorResponse.message)
      })
    }
  }

}

export class FileUploadOptions {
  controller?: string;
  action?: string;
  queryString?: string;
  explanation?: string;
  accept?: string;
}
