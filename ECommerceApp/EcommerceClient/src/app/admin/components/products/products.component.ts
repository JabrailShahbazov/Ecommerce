import {Component, OnInit} from '@angular/core';
import {NgxSpinnerService} from "ngx-spinner";
import {BaseComponent} from "../../../base/base.component";
import {HttpClientService} from "../../../services/common/http-client.service";

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent extends BaseComponent implements OnInit {

  constructor(spinner: NgxSpinnerService, private httpClientService: HttpClientService) {
    super(spinner);
  }

  ngOnInit(): void {

    this.httpClientService.get({controller: "product"},"9d0b0608-ecd2-4193-8859-f882f935c0f3").subscribe(
      data => {
        console.log(data)
      }
    )
  }

}
