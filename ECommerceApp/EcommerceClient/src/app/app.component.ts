import {Component} from '@angular/core';
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'EcommerceClient';
constructor(private tostr: ToastrService) {
}
  showToastr() {
    this.tostr.warning('dd','ddff')
  }
}
