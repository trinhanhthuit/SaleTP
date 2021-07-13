import { Component, OnInit } from '@angular/core';
import { map, filter, tap } from 'rxjs/operators';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { FrontEndService } from 'src/app/Service/frontEndService';
import { CategoryView, ProductView } from 'src/app/Shared/models/product';
import { environment } from 'src/environments/environment';
declare var $: any;
@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  constructor(
    private frontEndService: FrontEndService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
        //this.router.navigate([val.url]);
        let linkCode = '';
        this.route.params.subscribe(params => {
          linkCode = params['id'];
        });
        this.getData(linkCode);
      }

    });

    //router.events.subscribe((val) => {

    //  // see also 
    //  console.log(val);
    //  //this.router.navigate(["/same/route/path?refresh=1"]);
    //});
  }
  title = 'san-pham';
  langId = 'vi-VN';
  htmlToAddCategory = '';
  htmlPortfolioPprducts = '';
  categories: CategoryView[] = [];
  textdata: any = {};
  products: ProductView[] = [];
  saleBestProducts: ProductView[] = [];
  salePromotioProducts: ProductView[] = [];
  tempProducts: ProductView[] = [];
  categoryTrees: any[] = [];
  linkCode: any = '';
  categoryName: any = '';
  ngOnInit(): void {
    //let linkCode = '';
    //this.route.params.subscribe(params => {
    //  linkCode = params['id'];
    //});
    //this.getData(linkCode);
  }
  getData(linkCode: any): void {
    this.frontEndService.getDataProduct(this.langId, linkCode).subscribe(res => {
      if (res.IsSuccess) {
        this.products = [];
        if (res.Data.Products != null && res.Data.Products.length > 0) {
          this.products = res.Data.Products.map((data: any) => ({
            productId: data.ProductID,
            linkCode: data.LinkCode,
            productCode: data.ProductCode,
            productName: data.ProductName,
            categoryID: data.CategoryID,
            salePrice: data.SalePrice,
            discountPrice: data.DiscountPrice,
            //createdBy: data.CreatedBy,
            //createdDate: data.CreatedDate,
            //modifiBy: data.ModifiBy,
            //modifyDate: data.ModifyDate,
            //isHot: data.IsHot,
            //isActive: data.IsActive,
            imagePath: environment.urlApi + data.ImagePath,
            categoryCode: data.CategoryCode,
            categoryName: data.CategoryName
          }));
          this.categoryName = this.products[0].categoryName;
        }
        this.saleBestProducts = [];
        if (res.Data.SaleBestProducts != null && res.Data.SaleBestProducts.length > 0) {
          this.saleBestProducts = res.Data.SaleBestProducts.map((data: any) => ({
            productId: data.ProductID,
            linkCode: data.LinkCode,
            productCode: data.ProductCode,
            productName: data.ProductName,
            categoryID: data.CategoryID,
            salePrice: data.SalePrice,
            discountPrice: data.DiscountPrice,
            //createdBy: data.CreatedBy,
            //createdDate: data.CreatedDate,
            //modifiBy: data.ModifiBy,
            //modifyDate: data.ModifyDate,
            //isHot: data.IsHot,
            //isActive: data.IsActive,
            imagePath: environment.urlApi + data.ImagePath,
            categoryCode: data.CategoryCode
          }));
        }
        this.salePromotioProducts = [];
        if (res.Data.SalePromotioProducts != null && res.Data.SalePromotioProducts.length > 0) {
          this.salePromotioProducts = res.Data.SalePromotioProducts.map((data: any) => ({
            productId: data.ProductID,
            linkCode: data.LinkCode,
            productCode: data.ProductCode,
            productName: data.ProductName,
            categoryID: data.CategoryID,
            salePrice: data.SalePrice,
            discountPrice: data.DiscountPrice,
            //createdBy: data.CreatedBy,
            //createdDate: data.CreatedDate,
            //modifiBy: data.ModifiBy,
            //modifyDate: data.ModifyDate,
            //isHot: data.IsHot,
            //isActive: data.IsActive,
            imagePath: environment.urlApi + data.ImagePath,
            categoryCode: data.CategoryCode
          }));
        }

        this.tempProducts = this.products;
        this.categoryTrees = res.Data.Categories;
        //let htmlMenu = '';
        //for (const item of this.categoryTrees) {
        //  htmlMenu += '<li data-toggle="collapse" data-target="#menu_' + item.Catergory.CategoryID + '" class="collapsed" >';
        //  htmlMenu += '<a (click)="toggleImage()"><i class="fa fa-globe fa-lg"> </i> ' + item.Catergory.CategoryName + ' <span class="arrow"></span > </a>';
        //  htmlMenu += '</li>';
        //  htmlMenu += '<ul class="sub-menu collapse" id="menu_' + item.Catergory.CategoryID + '">';
        //  for (const itemSub of item.SubCategories) {
        //    htmlMenu += '<li>' + itemSub.CategoryName + ' </li>';
        //  }
        //  htmlMenu += '</ul>';
        //}
        //$('#menu-content').append(htmlMenu);



        this.categories = res.Data.Categories.map((data: any) => ({
          categoryId: data.CategoryId,
          categoryCode: data.CategoryCode,
          categoryName: data.CategoryName,
        }));


        //for (const item of this.categories) {
        //  this.htmlToAddCategory += '<li data-filter=".' + item.categoryCode + '">' + item.categoryName + '</li>';
        //}
        //$('#portfolio-flters').append(this.htmlToAddCategory);
        //for (const item of this.products) {
        //  this.htmlPortfolioPprducts += '<div class="col-lg-4 col-md-6 portfolio-item ' + item.categoryCode + '">';
        //  this.htmlPortfolioPprducts += '<div class="portfolio-wrap" >';
        //  this.htmlPortfolioPprducts += '<img src="' + item.imagePath1 + '" class="img-fluid" alt = "" >';
        //  this.htmlPortfolioPprducts += '</div>';
        //  this.htmlPortfolioPprducts += '<div class="text-center" >';
        //  this.htmlPortfolioPprducts += '<span> ' + item.productCode + ' <br />' + item.productName + '</span >';
        //  this.htmlPortfolioPprducts += '</div>';
        //  this.htmlPortfolioPprducts += '</div>';
        //}
        //$('#portfolio-prducts').append(this.htmlPortfolioPprducts);

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


  onProductByCategory(CategoryID: any): void {
    const tempProducts = this.tempProducts;
    this.products = tempProducts.filter(m => m.categoryID === CategoryID);
  }
}
