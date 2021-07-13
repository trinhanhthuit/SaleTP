import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { map, filter, tap } from 'rxjs/operators';
import { FrontEndService } from 'src/app/Service/frontEndService';
import { environment } from 'src/environments/environment';
declare var $: any;
@Component({
  selector: 'app-service',
  templateUrl: './service-detail.component.html',
  styleUrls: ['./service-detail.component.css']
})
export class ServiceDetailComponent implements OnInit {

  constructor(
    private frontEndService: FrontEndService,
    private route: ActivatedRoute
  ) { }
  title = 'Dịch vụ';
  langId = 'vi-VN';
  serviceRelations: any[] = [];
  service: any = {};
  textdata: any = {};
  routeSub: any;
  ngOnInit(): void {
    this.routeSub = this.route.params.subscribe(params => {
      console.log(params) //log the entire params object
      console.log(params['id']) //log the value of id
    });
    this.getData();
  }
  getData(): void {
    this.route.params.subscribe(params => {
      this.routeSub = params['id'];
      //console.log(params) //log the entire params object
      //console.log(params['id']) //log the value of id
    });
    this.frontEndService.getServiceDetailData(this.langId, this.routeSub).subscribe(res => {
      if (res.IsSuccess) {
        this.serviceRelations = res.Data.ServiceRelations;
        for (var i = 0; i < this.serviceRelations.length; i++) {
          this.serviceRelations[i].ImagePath = environment.urlApi + this.serviceRelations[i].ImagePath;
        }
        this.service = res.Data.Service;
        this.textdata = res.Data.TextData;
      }
    },
      response => {
        console.log("error", response);
      });
  }
  onDetail(linkCode:string): void {
    this.frontEndService.getServiceDetailData(this.langId, linkCode).subscribe(res => {
      if (res.IsSuccess) {
        this.serviceRelations = res.Data.ServiceRelations;
        for (var i = 0; i < this.serviceRelations.length; i++) {
          this.serviceRelations[i].ImagePath = environment.urlApi + this.serviceRelations[i].ImagePath;
        }
        this.service = res.Data.Service;
        this.textdata = res.Data.TextData;
      }
    },
      response => {
        console.log("error", response);
      });
  }
}

