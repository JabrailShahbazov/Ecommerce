import {NgxSpinnerService} from "ngx-spinner";

export class BaseComponent {
  constructor(private spinner: NgxSpinnerService) {
  }

  showSpinner(spinnerNameType: spinnerType) {
    this.spinner.show(spinnerNameType)
  }

  hideSpinner(spinnerNameType: spinnerType) {
    this.spinner.hide(spinnerNameType)
  }
}

export enum spinnerType {
  SquareLoader = "s1",
  "SquareJellyBox" = "s2",
  BallAtom = "s3"
}
