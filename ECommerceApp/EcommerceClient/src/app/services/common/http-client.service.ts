import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class HttpClientService {

  constructor(private httpClient: HttpClient,
              @Inject("baseUrl") private baseUrl: string) {
  }

  private url(requestParameters: Partial<RequestParameters>): string {

    return `${requestParameters.baseUrl ? requestParameters.baseUrl : this.baseUrl}/${requestParameters.controller}${requestParameters.action ? `/${requestParameters.baseUrl}` : ""}`
  }

  get<T>(requestParameters: Partial<RequestParameters>, id?: string): Observable<T> {
    let url: string = '';
    if (requestParameters.fullEndPoint) {
      url = requestParameters.fullEndPoint
    } else {
      debugger
      url = `${this.url(requestParameters)}${id != null ? `/${id}` : ""}`;
    }

    return this.httpClient.get<T>(url, {headers: requestParameters.headers})
  }

  post<T>(requestParameters: RequestParameters) {
  }

  put<T>(requestParameters: RequestParameters) {

  }

  delete<T>(requestParameters: RequestParameters) {

  }
}

export class RequestParameters {
  controller?: string;
  action?: string;
  headers?: HttpHeaders;
  baseUrl?: string;
  fullEndPoint?: string;
}
