import { Component, OnInit, AfterViewInit} from '@angular/core';
import { map, filter, tap } from 'rxjs/operators'
import { FrontEndService } from 'src/app/Service/frontEndService';
import {
  ProductView, CategoryView, AboutView, ServiceView, ImageView,
  TestimonialView, EmployeeView, WhyUsView, WhyUsDetailView, TextData
} from 'src/app/Shared/models/product';
import { environment } from 'src/environments/environment';
declare var $: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, AfterViewInit
{

  constructor(
    private frontEndService: FrontEndService,
  ) { }
  title = 'Trang chủ';
  langId = 'vi-VN';
  products: ProductView[] = [];
  productTemps: ProductView[] = [];
  categories: CategoryView[] = [];
  about: AboutView = {};
  services: ServiceView[] = [];
  htmlToAddCategory = '';
  htmlPortfolioPprducts = '';
  htmlToAddSubAbout = '';
  logoClients: ImageView[] = [];
  testimonials: TestimonialView[] = [];
  employees: EmployeeView[] = [];
  whyUs: WhyUsView = {};
  whyUsDetails: WhyUsDetailView[] = [];
  textDatas: TextData[] = [];
  textdataProduct: any = {};
  textdataAbout: any = {};
  textdataService: any = {};
  textdataEmployee: any = {};
  textdataTestimonial: any = {};
  textdataCall: any = {};
  ngOnInit(): void {
    this.getHomeData();
  }
  getHomeData(): void {
    this.frontEndService.getDataHome(this.langId).subscribe(res => {
      if (res.IsSuccess) {
        this.products = res.Data.Products.map((data: any) => ({
          productId: data.ProductID,
          productCode: data.ProductCode,
          productName: data.ProductName,
          salePrice: data.SalePrice,
          discountPrice: data.DiscountPrice,
          createdBy: data.CreatedBy,
          createdDate: data.CreatedDate,
          modifiBy: data.ModifiBy,
          modifyDate: data.ModifyDate,
          isHot: data.IsHot,
          isActive: data.IsActive,
          imagePath: environment.urlApi + data.ImagePath,
          categoryCode: data.CategoryCode,
          parentCode: data.ParentCode,
          linkCode:data.LinkCode
        }));
        this.productTemps = this.products;
        this.categories = res.Data.Categories.map((data: any) => ({
          categoryId: data.CategoryId,
          categoryCode: data.CategoryCode,
          categoryName: data.CategoryName,
        }))

        //for (const item of this.categories) {
        //  this.htmlToAddCategory += '<li data-filter=".' + item.categoryCode + '">' + item.categoryName + '</li>';
        //}
        //$('#portfolio-flters').append(this.htmlToAddCategory);
        //for (const item of this.products) {
        //  this.htmlPortfolioPprducts += '<div class="col-lg-3 col-md-6 portfolio-item ' + item.parentCode + '">';
        //  this.htmlPortfolioPprducts += '<div class="portfolio-wrap" >';
        //  this.htmlPortfolioPprducts += '<img src="' + item.imagePath + '" class="img-fluid" alt = "" >';
        //  this.htmlPortfolioPprducts += '</div>';
        //  this.htmlPortfolioPprducts += '<div class="portfolio-content text-center" >';
        //  this.htmlPortfolioPprducts += '<a href="/detail/' + item.productCode+'"><span> ' + item.productCode + ' <br />' + item.productName + '</span ></a>';
        //  this.htmlPortfolioPprducts += '</div>';
        //  this.htmlPortfolioPprducts += '</div>';
        //  console.log(this.htmlPortfolioPprducts);
        //}
        
       // $('#portfolio-prducts').append(this.htmlPortfolioPprducts);
        //about
        this.about = {
          aboutId: res.Data.About.AboutID,
          aboutTitle: res.Data.About.AboutTitle,
          aboutContent: res.Data.About.AboutContent,
          aboutContent2: res.Data.About.AboutContent2,
          aboutSubContent: res.Data.About.AboutSubContent,
        }

        let listSubContent = (<string>this.about.aboutSubContent).split('-');
        for (const item of listSubContent) {
          this.htmlToAddSubAbout += ' <li><i class="ri-check-double-line"></i>' + item + '</li>';
        }
        $('#about-subcontent').append(this.htmlToAddSubAbout);

        // service

        this.services = res.Data.Services.map((data: any) => ({
          linkCode: data.LinkCode,
          serviceId: data.ServiceID,
          serviceName: data.ServiceName,
          title: data.Title,
          shortContent: data.ShortContent,
          longContent: data.LongContent,
          imagePath1: data.ImagePath1,
          imagePath2: data.ImagePath2,
        }));

        this.logoClients = res.Data.LogoClients.map((data: any) => ({
          imageId: data.ImageID,
          imagePath: data.ImagePath
        }));

        this.testimonials = res.Data.Testimonials.map((data: any) => ({
          testimonialID: data.TestimonialID,
          imagePath1: data.ImagePath1,
          imagePath2: data.ImagePath2,
          customerName: data.CustomerName,
          position: data.Position,
          content: data.Content,
          subContent: data.SubContent,
        }));

        this.employees = res.Data.Employees.map((data: any) => ({
          employeeID: data.EmployeeID,
          imagePath1: data.ImagePath1,
          imagePath2: data.ImagePath2,
          linkTwitter: data.LinkTwitter,
          linkFacebook: data.LinkFacebook,
          linkInstagram: data.LinkInstagram,
          linkLinkedin: data.LinkLinkedin,
          employeeName: data.EmployeeName,
          position: data.Position,
          title: data.Title,
          content: data.Content,
          isActive: data.IsActive,
        }));

        this.whyUs = {
          id: res.Data.WhyUs.ID,
          imagePath1: res.Data.WhyUs.ImagePath1,
          imagePath2: res.Data.WhyUs.ImagePath2,
          sortOrder: res.Data.WhyUs.SortOrder,
          subContent: res.Data.WhyUs.SubContent,
          title: res.Data.WhyUs.Title,
          content: res.Data.WhyUs.Content,
          isActive: res.Data.WhyUs.IsActive,
        };

        this.whyUsDetails = res.Data.WhyUsDetails.map((data: any) => ({
          whyUSDetailID: data.WhyUSDetailID,
          imagePath1: data.ImagePath1,
          imagePath2: data.ImagePath2,
          sortOrder: data.SortOrder,
          subContent: data.SubContent,
          title: data.Title,
          content: data.Content,
          isActive: data.IsActive,
        }));

        this.textDatas = res.Data.TextDatas.map((data: any) => ({
          textID: data.TextID,
          langID: data.LangID,
          title: data.Title,
          shortContent: data.ShortContent,
          longContent: data.LongContent,
          pageCode: data.PageCode,
        }));
        this.textdataProduct = this.textDatas.filter(m => m.pageCode == 'HOME-PRODUCT')[0];
        this.textdataAbout = this.textDatas.filter(m => m.pageCode == 'HOME-ABOUT')[0];
        this.textdataService = this.textDatas.filter(m => m.pageCode == 'HOME-SERVICES')[0];
        this.textdataEmployee = this.textDatas.filter(m => m.pageCode == 'HOME-EMPLOYEE')[0];
        this.textdataTestimonial = this.textDatas.filter(m => m.pageCode == 'HOME-TESTIMONIAL')[0];
        this.textdataCall = this.textDatas.filter(m => m.pageCode == 'HOME-CALL')[0];

        
      }
    },
      response => {
        console.log("POST call in error", response);
      });
  }
  ngAfterViewInit(): void {
    $("portfolio-prducts").css("height", "370px");
    

  }
  onCategory(categoryCode: string): void {
    var products = this.productTemps;
    
    if (categoryCode == 'ALL') {
      this.products = products;
    }
    else {
      this.products = products.filter(item => item.categoryCode?.indexOf(categoryCode) != -1);
    }
    
  }
 
}




