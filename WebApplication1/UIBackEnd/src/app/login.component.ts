import { Component, OnInit } from '@angular/core';
import { map, filter, tap } from 'rxjs/operators';
import { BackEndService } from 'src/app/service/backEndService';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ImageModel } from 'src/app/model/product';


//import { UploadFile, UploadInput, UploadOutput } from 'ng-uikit-pro-standard';
declare var $: any;
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
  row: any = {
    UserName: '',
    PassWord: ''
  }

  constructor(
    private backEndService: BackEndService,
    private http: HttpClient,
  ) { }
  ngOnInit(): void {

  }
  onCheckLogin(): void {
    this.backEndService.checkLogin(this.row.UserName, this.row.Password).subscribe(res => {
      if (res.IsSuccess) {

      }
    },
      response => {
        console.log("Error", response);
      });
  }

}
