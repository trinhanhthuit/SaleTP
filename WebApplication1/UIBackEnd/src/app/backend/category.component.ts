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
  selector: 'app-product',
  templateUrl: './category.component.html',
})
export class CategoryComponent implements OnInit {
  imageForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(3)]),
    file: new FormControl('', [Validators.required]),
    fileSource: new FormControl('', [Validators.required])
  });

  constructor(
    private backEndService: BackEndService,
    private http: HttpClient,
  ) { }
  title = 'Nhóm sản phẩm';
  results: any[] = [];
  totalRows: number = 0;
  pages: number[] = [];
  selectedFiles: any[] = [];
  imageTemps: ImageModel[] = [];
  images: any[] = [];
  fileName: any;
  url: any;
  itemDelete: any = {};
  categories: any[] = [];
  row: any = {
    CategoryID: 0,
    CategoryCode:'',
    ParentID: 0,
    CategoryLangID: 0,
    LangID:'vi-VN'
  };
  data: any[] = [];
 
  filter: any = {
    searchString: '',
    isActive: -1,
    langId: 'vi-VN',
    isHot: -1
  }
  paging = {
    pages: this.pages,
    totalRows: 0,
    pageIndex: 0,
    pageSize: 20
  }
  onChangePageIndex(pageIndex: number): void {
    this.paging.pageIndex = pageIndex - 1;
    this.onSearch();

  }
  onChangePageSize(pageSize: number): void {
    this.paging.pageSize = pageSize;
    this.onSearch();

  }
  ngOnInit(): void {
    this.getInitData();
    this.onSearch();
  }

  getInitData(): void {
    this.categories = [];
    this.backEndService.getInitDataCategory(this.filter.langId).subscribe(res => {
      if (res.IsSuccess) {
        let categories = res.Data;
        for (var i = 0; i < categories.length; i++) {
          let category = categories[i];
          this.categories.push({
            CategoryID: category.CategoryID,
            CategoryName: category.CategoryName
          });
        }
      }
    },
      response => {
        console.log("POST call in error", response);
      });
  }
 
  onSearch(): void {
    this.backEndService.getDataCategory(this.filter.searchString, this.filter.isActive, this.filter.langId, this.paging.pageIndex, this.paging.pageSize).subscribe(res => {
      if (res.IsSuccess) {
        this.results = res.Data.Categories;
        this.paging.totalRows = res.Data.TotalRows;
        let pages = this.paging.totalRows / this.paging.pageSize;
        this.paging.pages = [];
        for (let i = 0; i < pages; i++) {
          this.paging.pages.push(i);
        }
      }
    },
      response => {
        console.log("POST call in error", response);
      });
  }
  showModal(): void {
    console.log(123);
  }
  onRefresh(): void {
    this.row = {
      CategoryID: 0,
      CategoryCode: '',
      ParentID: 0,
      CategoryLangID: 0,
      LangID: 'vi-VN',
      IsActive:true
    };
   
  }

  onAdd(): void {
    this.getInitData();
    this.onRefresh();
  }

  onSave(): void {
    this.row.LangID = this.filter.langId;
    this.backEndService.saveCategory(this.row).subscribe(res => {
      if (res.IsSuccess) {
        this.onRefresh();
        this.onSearch();
        $("#exampleModal").modal('hide');
      }
    },
      response => {
        console.log("POST call in error", response);
      });
  }
  onEdit(item: any): void {
   
    this.imageTemps = [];
    this.images = [];
    this.row = item;
  }




  onDelete(): void {
    this.backEndService.deleteCategory(this.itemDelete).subscribe(res => {
      if (res.IsSuccess) {
        $("#modalConfirm").modal('hide');
        this.onSearch();
      }
    },response => {console.log("POST call in error", response);});
  }


  

  onDeleteConfirm(item: any): void {
    this.itemDelete = item;
    $("#modalConfirm").modal();
  }




}
