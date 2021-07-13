import { Component, OnInit } from '@angular/core';
import { map, filter, tap } from 'rxjs/operators';
import { FrontEndService } from 'src/app/Service/frontEndService';
import { environment } from 'src/environments/environment';
declare var $: any;
@Component({
  selector: 'app-service',
  templateUrl: './service.component.html',
  styleUrls: ['./service.component.css']
})
export class ServiceComponent implements OnInit {

  constructor(
    private frontEndService: FrontEndService
  ) { }
  title = 'Dịch vụ';
  langId = 'vi-VN';
  services: any[] = [];
  textdata: any = {};
  ngOnInit(): void {
    this.getData();
  }
  getData(): void {
    this.frontEndService.getDataService(this.langId).subscribe(res => {
      if (res.IsSuccess) {
        this.services = res.Data.Services;
        for (var i = 0; i < this.services.length; i++) {
          this.services[i].ImagePath = environment.urlApi + this.services[i].ImagePath;
        }
        this.textdata = res.Data.TextData;
      }
    },
      response => {
        console.log("error", response);
      });
  }
}

