import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {BaseComponent} from "../../base/base.component";
import {Injector} from "@angular/core";

export class BaseDialog<T> extends BaseComponent {

  public dialogRef: MatDialogRef<T>
  public dialog: MatDialog

  constructor(injector: Injector) {
    super(injector)
    this.dialogRef = injector.get(MatDialogRef<T>)
    this.dialog = injector.get(MatDialog)

  }

  close() {
    this.dialogRef.close()
  }
}
