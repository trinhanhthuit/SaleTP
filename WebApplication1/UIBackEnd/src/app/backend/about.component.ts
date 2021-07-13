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
  selector: 'app-about',
  templateUrl: './about.component.html',
})
export class AboutComponent implements OnInit {
  imageForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(3)]),
    file: new FormControl('', [Validators.required]),
    fileSource: new FormControl('', [Validators.required])
  });

  constructor(
    private backEndService: BackEndService,
    private http: HttpClient,
  ) { }
  results: any[] = [];
  totalRows: number = 0;
  pages: number[] = [];
  selectedFiles: any[] = [];
  imageTemps: ImageModel[] = [];
  images: any[] = [];
  fileName: any;
  url: any;
  itemDelete: any = {};
  row: any = {
    AboutID: 0,
    AboutTitle:'',
    AboutContent: '',
    AboutContent2: '',
    AboutContent3: '',
    AboutSubContent:'',
    AboutLangID: '',
    LangID: 'vi-VN',
    IsActive: true,
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
    this.onSearch();
    $('.aboutTitle').summernote({ height: 100 });
    $('.aboutSubContent').summernote({ height: 100 });
    $('.aboutContent').summernote({ height: 100 });
    $('.aboutContent2').summernote({ height: 100 });
    $('.aboutContent3').summernote({ height: 400 });
  }
  onSearch(): void {
    this.backEndService.getDataAbout(this.filter.searchString, this.filter.isActive, this.filter.langId, this.paging.pageIndex, this.paging.pageSize).subscribe(res => {
      if (res.IsSuccess) {
        this.results = res.Data.Results;
        this.paging.totalRows = res.Data.TotalRows;
        let pages = this.totalRows / this.paging.pageSize;
        this.paging.pages = [];
        for (let i = 0; i < pages; i++) {
          this.paging.pages.push(i);
        }
      }
    },
      response => {
        console.log("Error", response);
      });
  }
  showModal(): void {
    console.log(123);
  }
  onRefresh(): void {
    this.row = {
      AboutID: 0,
      AboutName: '',
      AboutTitle: '',
      AboutContent: '',
      AboutContent2: '',
      AboutContent3: '',
      AboutSubContent: '',
      AboutLangID: '',
      LangID: 'vi-VN',
      IsActive:true,
    };
    $('.aboutTitle').summernote('code', '');
    $('.aboutContent').summernote('code', '');
    $('.aboutContent2').summernote('code', '');
    $('.aboutContent3').summernote('code', '');
    $('.aboutSubContent').summernote('code', '');
   
  }

  onAdd(): void {
    
    this.onRefresh();
  }

  onSave(): void {
    this.row.LangID = this.filter.langId;
    this.backEndService.saveAbout(this.row).subscribe(res => {
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
    this.row = item;
    $('.aboutTitle').summernote('code', this.row.AboutTitle);
    $('.aboutContent').summernote('code', this.row.AboutContent);
    $('.aboutContent2').summernote('code', this.row.AboutContent2);
    $('.aboutContent3').summernote('code', this.row.AboutContent3);
    $('.aboutSubContent').summernote('code', this.row.AboutSubContent);
  }
  onDelete(): void {
    this.backEndService.deleteAbout(this.itemDelete).subscribe(res => {
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
