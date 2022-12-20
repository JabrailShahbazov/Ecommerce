import { Injectable } from '@angular/core';
import {DialogPosition, MatDialog} from "@angular/material/dialog";
import {ComponentType} from "@angular/cdk/overlay";

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(private dialog: MatDialog) { }


  openDialog(dialogParameters: Partial<DialogParameters>): void {
    // @ts-ignore
    const dialogRef = this.dialog.open(dialogParameters.componentType, {
      width: dialogParameters.options?.width,
      height: dialogParameters.options?.height,
      position: dialogParameters.options?.position,
      data: dialogParameters.data,
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result == dialogParameters.data)
        { // @ts-ignore
          dialogParameters.afterClosed();
        }
    });
  }

}

export class DialogParameters {
  // @ts-ignore
  componentType: ComponentType<any>;
  data: any;
  // @ts-ignore
  afterClosed: () => void;
  options?: Partial<DialogOptions> = new DialogOptions();
}

export class DialogOptions {
  width?: string = "250px";
  height?: string;
  position?: DialogPosition;
}
