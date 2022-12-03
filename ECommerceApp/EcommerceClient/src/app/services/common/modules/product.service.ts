import {Injectable} from '@angular/core';
import {HttpClientService} from "../http-client.service";
import {CreateProduct} from "../../../contracts/admin/products/create-product";
import {UpdateProduct} from "../../../contracts/admin/products/update-product";
import {HttpErrorResponse} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClientService: HttpClientService) {
  }

  create(product: CreateProduct, successCallBack?: any, errorCallBack?: (errorMessage: any) => void) {
    this.httpClientService.post({controller: "product"}, product)
      .subscribe(data => {
        successCallBack()
      }, (errorResponse: HttpErrorResponse) => {
        const _error: Array<{ key: string, value: Array<string> }> = errorResponse.error;
        let message = ''
        _error.forEach((v, index) => {
          v.value.forEach((_v, _index) => {
            message += `${_v}, \n`
          });
        });
        if (errorCallBack) {
          errorCallBack(message)
        }
      });
  }

  update(product: UpdateProduct) {
    return this.httpClientService.post({controller: "product"}, product)
  }

  get<UpdateProduct>() {
    return this.httpClientService.get<UpdateProduct>({controller: "product"})
  }

  getById<UpdateProduct>(id: string | undefined, successCallBack?: any) {
    return this.httpClientService.get<UpdateProduct>({controller: "product"}, id)
      .subscribe(data => {
        successCallBack()
        return data
      })
  }
}
