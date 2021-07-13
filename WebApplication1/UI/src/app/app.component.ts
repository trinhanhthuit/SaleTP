import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { FrontEndService } from 'src/app/Service/frontEndService';
import { MenuView, Cart, AddToCart } from 'src/app/Shared/models/product';
import { Router, NavigationEnd } from "@angular/router";
declare var $: any;
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent implements OnInit, AfterViewInit {
  carts: Cart[] = [];
  addToCart: AddToCart = {
    quantity: 0,
    carts: []
  };
  message: string = '';
  msgNotification: string = '';
  constructor(
    private frontEndService: FrontEndService,
    private router: Router,
  ) {
    this.router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
        if (typeof (Storage) !== "undefined") {
          var addToCart = JSON.parse(localStorage.getItem("addToCart") || '{}');
          this.addToCart = {
            quantity: 0,
            carts: []
          }
          if (!(Object.keys(addToCart).length === 0))
            this.addToCart.quantity = addToCart.quantity;

        }
      }
    });
  }
  title = 'VIỄN THÔNG TẤN PHÁT';
  langId = 'vi-VN';
  menus: any[] = []
  ngOnInit(): void {
    this.getHomeData();
    this.getAddToCart();

  }
  getAddToCart(): void {
    if (typeof (Storage) !== "undefined") {
      this.addToCart = JSON.parse(localStorage.getItem("addToCart") || '{}');
    }
  }
  ngAfterViewInit(): void {
  }
  getHomeData(): void {
    this.frontEndService.getMenuData(this.langId).subscribe(res => {
      if (res.IsSuccess) {
        this.menus = res.Data.Menus;
      }
    },
      response => {
        console.log("POST call in error", response);
      });
  }
  gotolink(url: any, id: any): void {
    let myurl = '';
    if (id != '') {
      myurl = `${url}/${id}`;
    }
    else {
      myurl = `${url}`;
    }

    this.router.navigateByUrl(myurl);
  }

  showMsg(msg: string, type: string): void {
    var obj = $('#notification');
    obj.addClass("show "+type);
    setTimeout(function () { obj.removeClass("show"); }, 3000);
    this.msgNotification = msg;

  }

  formatddMMyyy(): any {
    var d = new Date();
    var str = d.getDate().toString();
    var month = d.getMonth() + 1;
    if (month.toString().length == 1)
      str = str + '0' + month.toString();
    else
      str = str + month.toString();
    str = str + d.getFullYear().toString().substring(2, d.getFullYear().toString().length);

    return str;

  }
}


