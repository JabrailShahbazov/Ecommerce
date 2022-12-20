import {Directive, ElementRef, EventEmitter, HostListener, Injector, Input, Output, Renderer2} from '@angular/core';
import {BaseComponent} from "../../base/base.component";
import {DeleteService} from "../../services/common/directives/admin/delete.service";
import {spinnerType} from "../../base/spinnerType";
import {DialogService} from "../../services/common/dialog.service";
import {FileUploadDialogComponent, FileUploadDialogState} from "../../dialogs/file-upload-dialog/file-upload-dialog.component";

declare var $: any

@Directive({
  selector: '[appDelete]'
})
export class DeleteDirective extends BaseComponent {
  @Input() id: string = ''
  @Input() controller: string = ''
  @Output() callBack: EventEmitter<any> = new EventEmitter<any>()

  constructor(injector: Injector,
              private element: ElementRef,
              private _render: Renderer2,
              private dialogService: DialogService,
              private httpClientService: DeleteService) {
    super(injector)
    const btn = _render.createElement('button')
    btn.classList.add('btn-danger')
    btn.classList.add('btn')
    btn.textContent = 'Delete';
    _render.appendChild(element.nativeElement, btn)
  }

  @HostListener('click')
  onClick() {
    this.dialogService.openDialog({
      componentType: FileUploadDialogComponent,
      data: DeleteDialogState.Yes,
      afterClosed: () => {
        const td: HTMLTableCellElement = this.element.nativeElement
        this.httpClientService.delete(this.id, this.controller, () => {
          this.toastrService.success("Deleted Element")
          $(td.parentElement).fadeOut(1000, () => {
            this.callBack.emit()
          })
        }, err => {
          this.hideSpinner(spinnerType.BallAtom)
          console.log(err)
        })
      }
    })

  }

}

export enum DeleteDialogState {
  Yes, No
}
