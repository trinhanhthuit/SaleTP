import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { map, filter, tap } from 'rxjs/operators'
import { FrontEndService } from 'src/app/Service/frontEndService';
import { CategoryView, ProductView, Cart } from 'src/app/Shared/models/product';
import { environment } from 'src/environments/environment';
import { AppComponent } from 'src/app/app.component';
declare var $: any;
@Component({
  selector: 'app-product',
  templateUrl: './news.component.html',
})
export class NewsComponent implements OnInit {

  constructor(
    private frontEndService: FrontEndService,
    private route: ActivatedRoute,
    private router: Router,
    private appConbonent: AppComponent
  ) {
    this.router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
        this.getData();
      }

    });
  }
  title = 'chi tiết sản phẩm';
  langId = 'vi-VN';
  htmlToAddCategory = '<li data-filter="*" class="filter-active">Tất cả</li>';
  htmlPortfolioPprducts = '';
  imagePathDefault: any = {};
  imagePaths: any[] = [];
  categories: CategoryView[] = [];
  textdata: any = {};
  productDetail: any = {};
  productPromtions: any[] = [];
  productHots: any[] = [];
  linkCode: any = '';
  tempProducts: ProductView[] = [];
  categoryTrees: any[] = [];
  cart: Cart = {
    productId: '',
    productCode: '',
    productName: '',
    categoryId: 0,
    categoryName: '',
    price: 0,
    imagePath: '',
    quantity: 1,
    totalPrice:0
  };
  @Output() messageEvent = new EventEmitter<any>();

  sendMessage(): void {
    this.messageEvent.emit(this.cart);
  }
  test: string = '';
  ChildTestCmp() {
    this.test = "I am child component!";
  }
  ngOnInit(): void {
    //this.getData();
    this.sendMessage();
    this.frontEndService.changeMessage("chào nha");

  }
  getData(): void {
    this.route.params.subscribe(params => {
      this.linkCode = params['id'];
    });

    this.frontEndService.getProductDetail(this.langId, '', this.linkCode).subscribe(res => {
      if (res.IsSuccess) {
        this.cart.quantity = 1;
        this.productDetail = res.Data.ProductDetail;
        var images = this.productDetail.Images.map((s: any) => ({
          ImagePath: environment.urlApi + s.ImagePath,
          IsDefault: s.IsDefault
        }));

        this.imagePathDefault = images.filter((s: any) => s.IsDefault)[0];
        this.imagePaths = images;

        this.productPromtions = res.Data.ProductPromotions.map((s: any) => ({
          ProductCode: s.ProductCode,
          ProductName: s.ProductName,
          SalePrice: s.SalePrice,
          DiscountPrice: s.DiscountPrice,
          ImagePath: environment.urlApi + s.ImagePath,
          LinkCode: s.LinkCode
        }));

        this.productHots = res.Data.ProductHots.map((s: any) => ({
          ProductCode: s.ProductCode,
          ProductName: s.ProductName,
          SalePrice: s.SalePrice,
          DiscountPrice: s.DiscountPrice,
          ImagePath: environment.urlApi + s.ImagePath,
          LinkCode: s.LinkCode
        }));;

        this.textdata = {
          textID: res.Data.TextData.TextID,
          langID: res.Data.TextData.LangID,
          title: res.Data.TextData.Title,
          shortContent: res.Data.TextData.ShortContent,
          longContent: res.Data.TextData.LongContent,
          pageCode: res.Data.TextData.PageCode,
        };
      }
    },
      response => {
        console.log("POST call in error", response);
      });
  }

  onMinusQuantity(): void {
    if (this.cart.quantity <= 1) {
      return;
    }
    this.cart.quantity -= 1;
  }
  onPlusQuantity(): void {
    this.cart.quantity += 1;
  }
  onChangeImage(imagePath: any): void {
    var tempImagePaths = this.imagePaths.map((item: any) => ({
      ImagePath: item.ImagePath,
      IsDefault: item.IsDefault,
    }));
    this.imagePathDefault.ImagePath = imagePath;
    this.imagePaths = tempImagePaths;
  }
  addToCart(): void {

    var products = this.appConbonent.addToCart.carts.filter((s: Cart) => s.productId == this.productDetail.ProductID);

    if (products.length == 0) {
      
      this.cart = {
        productId: this.productDetail.ProductID,
        productCode: this.productDetail.ProductCode,
        productName: this.productDetail.ProductName,
        price: this.productDetail.DiscountPrice,
        categoryId: this.productDetail.CategoryID,
        categoryName: this.productDetail.CategoryName,
        imagePath: this.productDetail.ImagePath,
        quantity: this.cart.quantity,
        totalPrice: this.productDetail.DiscountPrice * this.cart.quantity
      }
      this.appConbonent.addToCart.quantity = this.appConbonent.addToCart.quantity + 1;
      this.appConbonent.addToCart.carts.push(this.cart);
    }
    else {
      products[0].quantity = this.cart.quantity + products[0].quantity;
    }
    localStorage.setItem('addToCart', JSON.stringify(this.appConbonent.addToCart));
    //localStorage.addToCart = this.appConbonent.addToCart;

  }
}
