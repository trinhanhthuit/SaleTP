import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/shared/base.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment'

@Injectable()
export class BackEndService extends BaseService {
  protected url = '';
  constructor(http: HttpClient) {
    super(http);
    //this.url = environment.urlApi;
    this.url ='';
  }
  getInitData(langid: string): Observable<any> {
    const url = this.url + '/api/product/GetInitData?langId=' + langid;
    return this.get(url);
  }
  getDataProduct(searchString: string, isActive: number, langid: string, pageIndex: number, pageSize: number): Observable<any> {
    const url = this.url + '/api/product/GetProduct?searchString=' + searchString + '&isActive=' + isActive + '&langId=' + langid + '&pageIndex=' + pageIndex + '&pageSize=' + pageSize;
    return this.get(url);
  }
  getImage(productId: string): Observable<any> {
    const url = this.url + '/api/product/GetImage?pathId=' + productId;
    return this.get(url);
  }
  saveProduct(row: any): Observable<any> {
    const url = this.url + '/api/product/SaveProduct';
    return this.post(url, row);
  }
  uploadFile(file: any, urlUpload: string): Observable<any> {
    const url = this.url + '/api/FileUpload/UploadFiles?urlUpload=' + urlUpload;
    return this.post(url, file);
  }
  deleteProduct(row: any): Observable<any> {
    const url = this.url + '/api/product/DeleteProduct';
    return this.post(url, row);
  }
  //*********Category*********
  getDataCategory(searchString: string, isActive: number, langid: string, pageIndex: number, pageSize: number): Observable<any> {
    const url = this.url + '/api/product/GetCategory?searchString=' + searchString + '&isActive=' + isActive + '&langId=' + langid + '&pageIndex=' + pageIndex + '&pageSize=' + pageSize;
    return this.get(url);
  }
  getInitDataCategory(langid: string): Observable<any> {
    const url = this.url + '/api/product/GetInitDataCategory?langId=' + langid;
    return this.get(url);
  }
  saveCategory(row: any): Observable<any> {
    const url = this.url + '/api/product/SaveCategory';
    return this.post(url, row);
  }
  deleteCategory(row: any): Observable<any> {
    const url = this.url + '/api/product/DeleteCategory';
    return this.post(url, row);
  }
  //*********End Category*********

  //*********Service*********
  getDataService(searchString: string, isActive: number, langid: string, pageIndex: number, pageSize: number): Observable<any> {
    const url = this.url + '/api/product/GetService?searchString=' + searchString + '&isActive=' + isActive + '&langId=' + langid + '&pageIndex=' + pageIndex + '&pageSize=' + pageSize;
    return this.get(url);
  }
  saveService(row: any): Observable<any> {
    const url = this.url + '/api/product/SaveService';
    return this.post(url, row);
  }
  deleteService(row: any): Observable<any> {
    const url = this.url + '/api/product/DeleteService';
    return this.post(url, row);
  }
  //*********End Service*********
  //*********About*********
  getDataAbout(searchString: string, isActive: number, langid: string, pageIndex: number, pageSize: number): Observable<any> {
    const url = this.url + '/api/product/about?searchString=' + searchString + '&isActive=' + isActive + '&langId=' + langid + '&pageIndex=' + pageIndex + '&pageSize=' + pageSize;
    return this.get(url);
  }
  saveAbout(row: any): Observable<any> {
    const url = '/api/product/about';
    return this.post(url, row);
  }
  deleteAbout(row: any): Observable<any> {
    const url = this.url + '/api/product/DeleteAbout';
    return this.post(url, row);
  }
  //*********End About*********

  //*********Contact*********
  getDataContact(searchString: string, fromDate: string, toDate: string, status: number, pageIndex: number, pageSize: number): Observable<any> {
    const url = this.url + '/api/product/Contact?searchString=' + searchString + '&status=' + status + '&fromDate=' + fromDate + '&toDate=' + toDate + '&pageIndex=' + pageIndex + '&pageSize=' + pageSize;
    return this.get(url);
  }
  saveContact(row: any): Observable<any> {
    const url = this.url + '/api/product/Contact';
    return this.post(url, row);
  }
  deleteContact(row: any): Observable<any> {
    const url = this.url + '/api/product/DeleteContact';
    return this.post(url, row);
  }
  //*********End Contact*********

  //********Login********
  checkLogin(row:any): Observable<any> {
    const url = this.url + '/api/login/checklogin';
    return this.post(url, row);
  }

}
