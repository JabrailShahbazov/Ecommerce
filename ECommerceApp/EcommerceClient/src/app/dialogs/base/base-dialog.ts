import {MatDialogRef} from "@angular/material/dialog";
import {BaseComponent} from "../../base/base.component";
import {Injector} from "@angular/core";

export class BaseDialog<T> extends BaseComponent {

  private dialogRef: MatDialogRef<T>

  constructor(injector: Injector) {
    super(injector)
    this.dialogRef = injector.get(MatDialogRef<T>)
  }

  close() {
    this.dialogRef.close()
  }
}
