import { Component, OnInit } from '@angular/core';
import { map, filter, tap } from 'rxjs/operators';
import { BackEndService } from 'src/app/service/backEndService';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ImageModel } from 'src/app/model/product';
import { formatDate } from '@angular/common';
import { cloneDeep } from 'lodash';
import { ContactModel } from 'src/app/model/contact'

//import { UploadFile, UploadInput, UploadOutput } from 'ng-uikit-pro-standard';
declare var $: any;
@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
})
export class ContactComponent implements OnInit {
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
  nowDate: Date = new Date();
  row: ContactModel = {
    ContactID: 0,
    Subject: '',
    FullName: '',
    Email: '',
    PhoneNumber: '',
    Contents: '',
    Status: -1,
    CreatedBy: 0,
    ModifyBy: 0,
    CreatedDate: this.nowDate,
    ModifyDate: this.nowDate
  };
  data: any[] = [];

  filter: any = {
    searchString: '',
    status: 1,
    fromDate: new Date('2021-03-01').toISOString().split('T')[0],
    toDate: new Date().toISOString().split('T')[0]
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
    this.paging.pageIndex = 0;
    this.onSearch();
  }

  onChangePageNext(): void {
    if (this.paging.pageIndex < this.paging.pages.length-1) {
      this.paging.pageIndex = this.paging.pageIndex + 1;
      this.onSearch();
    }
  }

  onChangePagePrevious(): void {
    if (this.paging.pageIndex > 0) {
      this.paging.pageIndex = this.paging.pageIndex - 1;
      this.onSearch();
    }

  }

  ngOnInit(): void {
    this.onSearch();
    $('.contents').summernote({ height: 100 });
  }

  onSearch(): void {
    let fromDate = formatDate(this.filter.fromDate, 'yyyy-MM-dd', 'en');
    let toDate = formatDate(this.filter.toDate, 'yyyy-MM-dd', 'en');
    this.backEndService.getDataContact(this.filter.searchString, fromDate, toDate, this.filter.status, this.paging.pageIndex, this.paging.pageSize).subscribe(res => {
      if (res.IsSuccess) {
        this.results = res.Data.Results;
        this.paging.totalRows = res.Data.TotalRows;
        let pages = this.paging.totalRows / this.paging.pageSize;
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
      ContactID: 0,
      Subject: '',
      FullName: '',
      Email: '',
      PhoneNumber: '',
      Contents: '',
      Status: -1,
      CreatedBy: 0,
      ModifyBy: 0,
      CreatedDate: this.nowDate,
      ModifyDate: this.nowDate
    };
    $('.contents').summernote('code', '');
  }

  onAdd(): void {
    this.onRefresh();
  }

  onSave(): void {
    this.backEndService.saveContact(this.row).subscribe(res => {
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
  //copy(item:): void {

  //}
  onEdit(item: ContactModel): void {
    this.row = cloneDeep(item);
    this.row.ModifyBy = 0;
    this.row.CreatedBy = 0;
    this.row.ModifyDate = 
    $('.contents').summernote('code', this.row.Contents);
  }
  onDelete(): void {
    this.backEndService.deleteContact(this.itemDelete).subscribe(res => {
      if (res.IsSuccess) {
        $("#modalConfirm").modal('hide');
        this.onSearch();
      }
    }, response => { console.log("POST call in error", response); });
  }
  onDeleteConfirm(item: any): void {
    this.itemDelete = item;
    $("#modalConfirm").modal();
  }
}
