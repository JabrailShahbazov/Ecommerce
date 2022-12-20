import {Injectable} from '@angular/core';
import {HttpClientService} from "../../http-client.service";
import {firstValueFrom} from "rxjs";
import {DialogService} from "../../dialog.service";

@Injectable({
  providedIn: 'root'
})
export class DeleteService {

  constructor(private httpClientService: HttpClientService) {
  }


  delete(id: string, controller: string, successCallBack?: any, errorCallBack?: (errorMessage: any) => void) {
    const obs = this.httpClientService.delete({
      controller: controller
    }, id);

    firstValueFrom(obs).then(r => successCallBack()).catch(err => errorCallBack ? errorCallBack(err) : "")
  }
}
