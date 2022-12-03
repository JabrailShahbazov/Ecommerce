import {Component, Injector, OnInit} from '@angular/core';
import {NgxSpinnerService} from "ngx-spinner";
import {BaseComponent} from "../../../base/base.component";

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent extends BaseComponent implements OnInit {

  constructor(injector: Injector) {
    super(injector);
  }

  ngOnInit(): void {
  }

}
