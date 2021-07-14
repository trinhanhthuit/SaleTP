import { Component, OnInit } from '@angular/core';
import { map, filter, tap } from 'rxjs/operators';
import { BackEndService } from 'src/app/service/backEndService';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ImageModel } from 'src/app/model/product';
import { UtilService } from 'src/app/shared/util.service';

//import { UploadFile, UploadInput, UploadOutput } from 'ng-uikit-pro-standard';
declare var $: any;
@Component({
  selector: 'app-product',
  templateUrl: './service.component.html',
})
export class ServiceComponent implements OnInit {
  imageForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(3)]),
    file: new FormControl('', [Validators.required]),
    fileSource: new FormControl('', [Validators.required])
  });

  constructor(
    private backEndService: BackEndService,
    private http: HttpClient,
    private utilService: UtilService
  ) { }
  title = 'san-pham';
  results: any[] = [];
  totalRows: number = 0;
  pages: number[] = [];
  selectedFiles: any[] = [];
  imageTemps: ImageModel[] = [];
  images: any[] = [];
  fileName: any;
  url: any;
  itemDelete: any = {};
  urlUpload: string = '/upload/service/';
  row: any = {
    CategoryID: '-1',
  };
  data: any[] = [];
  image: any = {

  }
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
    $('.shortContent').summernote({ height: 200 });
    $('.longContent').summernote({ height: 600 });

    this.onSearch();

  }

  onSearch(): void {
    this.backEndService.getDataService(this.filter.searchString, this.filter.isActive, this.filter.langId, this.paging.pageIndex, this.paging.pageSize).subscribe(res => {
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

  }
  onRefresh(): void {
    this.row = {
      ServiceID: '',
      ServiceName: '',
      ServiceCode: '',
      LinkCode: '',
      IsActive: true,
      Title: '',
      ShortContent: '',
      LongContent: '',
      LangID: this.filter.langId
    };
    this.imageTemps = [];
    this.selectedFiles = [];
    $('#file').val('');

    $('.shortContent').summernote('code', '');
    $('.longContent').summernote('code', '');
  }

  onAdd(): void {
    this.onResetTab();
    this.onRefresh();
  }

  onSave(): void {
    this.row.LangID = this.filter.langId;
    this.row.ShortContent = $('.shortContent').summernote('code');
    this.row.LongContent = $('.longContent').summernote('code');
    let model = {
      Service: this.row,
      Images: this.images
    }
    this.backEndService.saveService(model).subscribe(res => {
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
    this.onResetTab();
    this.imageTemps = [];
    this.images = [];
    this.row = item;
    this.backEndService.getImage(item.ServiceID).subscribe(res => {
      if (res.IsSuccess) {
        let images = res.Data;
        for (var i = 0; i < images.length; i++) {
          this.imageTemps = images.map((data: any) => ({
            ImageID: data.ImageID,
            CategoryID: data.CategoryID,
            ImagePath: environment.urlApi + data.ImagePath,
            PathID: data.PathID,
            SortOrder: data.SortOrder,
            IsDefault: data.IsDefault,
            File: ''

          }));
        }
        $('.shortContent').summernote('code', this.row.ShortContent);
        $('.longContent').summernote('code', this.row.LongContent);
      }
    },
      response => {
        console.log("POST call in error", response);
      });

  }

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      for (let i = 0; i < event.target.files.length; i++) {
        var reader = new FileReader();
        const file = event.target.files[i];
        // this.selectedFiles.push(file);
        reader.onload = (event: any) => {

          this.imageTemps.push({
            ImageID: '',
            ImagePath: event.target.result,
            CategoryID: 0,
            PathID: '',
            SortOrder: 0,
            IsDefault: false,
            File: file
          }
          );
        }
        reader.readAsDataURL(event.target.files[i]);

      }
    }
  }

  onUpload() {
    let formData = new FormData();

    let imageTempOlds = this.imageTemps.filter(m => m.ImageID != '');
    for (let i = 0; i < imageTempOlds.length; i++) {
      let imageOld = {
        ImageID: imageTempOlds[i].ImageID,
        ImagePath: imageTempOlds[i].ImagePath.replace(environment.urlApi + this.urlUpload, ''),
        CategoryID: 1,
        PathID: imageTempOlds[i].PathID,
        IsDefault: imageTempOlds[i].IsDefault,
        SortOrder: i + 1
      }
      this.images.push(imageOld);
    }
    let imageTemps = this.imageTemps.filter(m => m.ImageID == '');
    for (let i = 0; i < imageTemps.length; i++) {
      let _filename = this.makeid(10);

      let fileName = _filename + '.' + imageTemps[i].File.type.replace("image/", "").replace('jpeg', 'jpg');
      let image = {
        ImageID: '',
        ImagePath: this.urlUpload + fileName,
        CategoryID: 1,
        PathID: '',
        IsDefault: imageTemps[i].IsDefault,
        SortOrder: i + 1

      }
      this.images.push(image);
      formData.append("fileName", _filename);
      formData.append("file", imageTemps[i].File);
    }

    this.backEndService.uploadFile(formData, this.urlUpload).subscribe(res => {
      if (res.IsSuccess) {
        console.log('thanh cong');
        this.onSave();
      }
    },
      response => {
        console.log("POST call in error", response);
      });

  }

  onDelete(): void {
    this.backEndService.deleteService(this.itemDelete).subscribe(res => {
      if (res.IsSuccess) {
        $("#modalConfirm").modal('hide');
        this.onSearch();
      }
    }, response => { console.log("POST call in error", response); });
  }

  makeid(length: number): string {
    var result = [];
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++) {
      result.push(characters.charAt(Math.floor(Math.random() *
        charactersLength)));
    }
    return result.join('');
  }
  onDeleteImage(index: number): void {
    this.imageTemps.splice(index, 1);
  }
  onResetTab(): void {
    $('.nav-tabs .nav-link').removeClass('active');
    $(".nav-tabs .nav-link").attr("aria-selected", "false");
    $('.nav-tabs .nav-link').first().addClass('active');
    $('.nav-tabs .nav-link').first().attr("aria-selected", "true");

    $('.tab-content .tab-pane').removeClass('active show');
    $('.tab-content .tab-pane').first().addClass('active show');

  }

  onDeleteConfirm(item: any): void {
    this.itemDelete = item;
    $("#modalConfirm").modal();
  }
  onKey(row: any, value: any): void {
    let valueStr = this.utilService.removeSymbol(value);
    row.LinkCode = valueStr.split(" ").join("-").toLowerCase();
  }



}
