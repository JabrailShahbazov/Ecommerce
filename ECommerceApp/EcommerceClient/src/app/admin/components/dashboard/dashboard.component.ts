import {Component, Injector, OnInit} from '@angular/core';
import {NgxSpinnerService} from "ngx-spinner";
import {BaseComponent} from "../../../base/base.component";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent extends BaseComponent implements OnInit {

  constructor(injector: Injector) {
    super(injector);
  }


  ngOnInit(): void {
    this.toastrService.info("Dashboard Work")
  }

}
