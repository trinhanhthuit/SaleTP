import { conditionallyCreateMapObjectLiteral } from '@angular/compiler/src/render3/view/util';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { Subscription } from 'rxjs';
import { map, filter, tap } from 'rxjs/operators';
import { FrontEndService } from 'src/app/Service/frontEndService';
import { environment } from 'src/environments/environment';
import { Cart } from 'src/app/Shared/models/product';
import { AppComponent } from 'src/app/app.component';
declare var $: any;
@Component({
  selector: 'app-service',
  templateUrl: './cart.component.html',
})
export class CartComponent implements OnInit {
  products: Cart[] = [];
  cartProduct: any = {
    products: this.products,
    subTotal: 0,
    discount: 0,
    total: 0
  };
  orderDetail: any = {
    OrderDetailID: 0,
    OrderID: 0,
    ProductID: '',
    SalePrice: 0,
    DiscountPrice: 0,
    TotalPrice: 0,
    Quantity: 0,
  };
  booking: any = {
    order: {
      OrderID: 0,
      VoucherCode: '#' + this.appConbonent.formatddMMyyy(),
      CreatedDate: new Date(),
      CreatedBy: 1,

      DeliveryStatus: 1,
      IsActive: true,
      CustomerID: 0,
    },

    orderDetails: this.orderDetail = [],
    customer: {
      CustomerID: 0,
      CustomerCode: '',
      CustomerName: 'Trinh Anh Thu',
      Address: '606 Hồ Học Lãm, BTĐ B',
      Phone: '0923912038123',
      ProvinceID: 30,
      DistrictID: 13,
      WardID: 0
    }

  }
  steps: number = 1;
  title: string = 'GIỎ HÀNG';
  constructor(
    private frontEndService: FrontEndService,
    private route: ActivatedRoute,
    private router: Router,
    private appConbonent: AppComponent
  ) {
    this.router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
        if (typeof (Storage) !== "undefined") {
          var addToCart = JSON.parse(localStorage.getItem("addToCart") || '{}');
          if (!(Object.keys(addToCart).length === 0)) {
            this.products = addToCart.carts;
            for (var i = 0; i < addToCart.carts.length; i++) {
              this.products[i].imagePath = environment.urlApi + addToCart.carts[i].imagePath.replace('http://localhost:44343', '');
            }
            this.cartProduct.products = this.products;
            this.calTotal();
          }
        }
      }
    });
  }
  langId = 'vi-VN';
  serviceRelations: any[] = [];
  service: any = {};
  textdata: any = {};
  routeSub: any;
  provinces: any[] = [];
  districts: any[] = [];
  ngOnInit(): void {
    this.getData();
  }
  getData(): void {
    this.frontEndService.getOrder().subscribe(res => {
      if (res.IsSuccess) {
        this.provinces = res.Data.Provinces;
        this.districts = res.Data.Districts;
      }
    },
      response => {
        console.log("POST call in error", response);
      });
  }
  calTotal(): void {
    var subTotal = 0;
    for (var i = 0; i < this.cartProduct.products.length; i++) {
      this.cartProduct.products[i].totalPrice = this.cartProduct.products[i].price * this.cartProduct.products[i].quantity;
      subTotal += this.cartProduct.products[i].totalPrice;
    }
    this.cartProduct.subTotal = subTotal;
    this.cartProduct.total = this.cartProduct.subTotal - this.cartProduct.discount;
    this.updateToCar();
    //localStorage.setItem('addToCart', JSON.stringify(this.appConbonent.addToCart));
  }
  deleteCart(index: any): void {
    this.cartProduct.products.splice(index, 1);
    this.calTotal();

  }
  updateToCar(): void {
    this.appConbonent.addToCart.quantity = this.cartProduct.products.length;
    this.appConbonent.addToCart.carts = this.cartProduct.products;
    localStorage.setItem('addToCart', JSON.stringify(this.appConbonent.addToCart));
  }
  onOrderContinue(step: number): void {
    this.steps = step;
    switch (this.steps) {
      case 2: this.title = 'ĐẶT HÀNG';
        break;
      case 3: this.title = 'HOÀN TẤT';
        this.saveOrder();

        //this.cartProduct.products = [];
        break;
      default: this.title = 'GIỎ HÀNG';
        break;
    }

  }

  changeProvince(): void {

  }
  saveOrder(): void {

    this.booking.orderDetails = this.cartProduct.products.map((s: any) => ({
      OrderDetailID: 0,
      OrderID: 0,
      ProductID: s.productId,
      SalePrice: s.price,
      DiscountPrice: 0,
      TotalPrice: s.totalPrice,
      Quantity: s.quantity,
    }));
      this.frontEndService.saveOrder(this.booking.order).subscribe(res => {
        if (res.IsSuccess) {
          localStorage.removeItem("addToCart");
          this.appConbonent.addToCart = {
            quantity: 0,
            carts: []
          };
        }
      },
        response => {
          console.log("POST call in error", response);
        });
  }

}

