import {Injectable} from '@angular/core';
import {HttpClientService} from "../http-client.service";
import {CreateProduct} from "../../../contracts/admin/products/create-product";
import {UpdateProduct} from "../../../contracts/admin/products/update-product";
import {HttpErrorResponse} from "@angular/common/http";
import {ListProduct} from "../../../contracts/admin/products/list-product";

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

  async get(successCallBack?: () => void, errorCallBack?: (errorMessage: any) => void): Promise<ListProduct[] | undefined> {
    const products = this.httpClientService.get<ListProduct[]>({controller: "product"}).toPromise();
    if (errorCallBack) {
      if (successCallBack) {
        products.then(d => successCallBack())
          .catch((errorResponse: HttpErrorResponse) => errorCallBack(errorResponse.message))
      }
    }
    return await products;
  }

  async getById(id: string, successCallBack?: () => void, errorCallBack?: (errorMessage: any) => void): Promise<ListProduct | undefined> {
    const products = this.httpClientService.get<ListProduct>({controller: "product"}, id).toPromise();
    if (errorCallBack) {
      if (successCallBack) {
        products.then(d => successCallBack())
          .catch((errorResponse: HttpErrorResponse) => errorCallBack(errorResponse.message))
      }
    }
    return await products;
  }
}
