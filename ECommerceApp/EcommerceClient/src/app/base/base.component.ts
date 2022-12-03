import {NgxSpinnerService} from "ngx-spinner";
import {Injector} from "@angular/core";
import {ToastrService} from "ngx-toastr";
import {spinnerType} from "./spinnerType";

export class BaseComponent {
  spinnerService: NgxSpinnerService;
  toastrService: ToastrService

  constructor(injector: Injector) {
    this.spinnerService = injector.get(NgxSpinnerService)
    this.toastrService = injector.get(ToastrService)
  }

  showSpinner(spinnerNameType: spinnerType) {
    this.spinnerService.show(spinnerNameType)
  }

  hideSpinner(spinnerNameType: spinnerType) {
    this.spinnerService.hide(spinnerNameType)
  }
}

