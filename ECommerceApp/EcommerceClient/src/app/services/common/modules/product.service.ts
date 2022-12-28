import {Injectable} from '@angular/core';
import {HttpClientService} from "../http-client.service";
import {CreateProduct} from "../../../contracts/admin/products/create-product";
import {UpdateProduct} from "../../../contracts/admin/products/update-product";
import {HttpErrorResponse} from "@angular/common/http";
import {ProductListDto} from "../../../contracts/admin/products/list-product";
import {PaginationDto} from "../../../contracts/common/pagination-dto";
import {PagedResultDto} from "../../../contracts/common/paged-result-dto";
import {firstValueFrom, Observable} from "rxjs";
import {ListProductImage} from "../../../contracts/admin/products/list-product-image";

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

  update(product: UpdateProduct, successCallBack?: any, errorCallBack?: (errorMessage: any) => void) {
    this.httpClientService.put({controller: "product"}, product)
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

  async get(pagination: PaginationDto, successCallBack?: () => void, errorCallBack?: (errorMessage: any) => void): Promise<PagedResultDto<ProductListDto[]> | undefined> {
    const products = this.httpClientService.get<PagedResultDto<ProductListDto[]>>({
      controller: "product",
      queryString: `page=${pagination.page}&size=${pagination.size}`
    }).toPromise();

    // @ts-ignore
    products.then(d => successCallBack())
      // @ts-ignore
      .catch((errorResponse: HttpErrorResponse) => errorCallBack(errorResponse.message))

    return await products;
  }

  async getById(id: string, successCallBack?: () => void, errorCallBack?: (errorMessage: any) => void): Promise<ProductListDto | undefined> {
    const products = this.httpClientService.get<ProductListDto>({controller: "product"}, id).toPromise();
    if (errorCallBack) {
      if (successCallBack) {
        products.then(d => successCallBack())
          .catch((errorResponse: HttpErrorResponse) => errorCallBack(errorResponse.message))
      }
    }
    return await products;
  }

  async getImages(id: string): Promise<[ListProductImage]> {
    const getObservable: Observable<[ListProductImage]> = this.httpClientService.get<[ListProductImage]>({
      action: "getproductimage",
      controller: "product"
    }, id);

    return await firstValueFrom(getObservable);
  }

}
