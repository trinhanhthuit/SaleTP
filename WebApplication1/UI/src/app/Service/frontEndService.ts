import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/Shared/base.service'
import { Observable, BehaviorSubject  } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment'

@Injectable()
export class FrontEndService extends BaseService {
  messageSource = new BehaviorSubject<string>("default message");
  currentMessage = this.messageSource.asObservable();

  protected url = '';
  constructor(http: HttpClient) {
    super(http);
    this.url = environment.urlApi;
  }

  changeMessage(message:any) {
    this.messageSource.next(message);
  }

  getDataHome(langid: string): Observable<any> {
    return this.get(this.url + '/api/values/GetHomeData?langId=' + langid);
  }
  getDataAbout(langid: string): Observable<any> {
    return this.get(this.url +'/api/values/GetDataAbout?langId=' + langid);
  }

  getDataProduct(langid: string, cateid: string): Observable<any> {
    return this.get(this.url +'/api/values/GetDataProduct?langId=' + langid + '&cateid=' + cateid);
  }
  getProductDetail(langid: string, linkCategory: string, linkCode: string): Observable<any> {
    return this.get(this.url +'/api/values/GetProductDetail?langId=' + langid + '&linkCategory=' + linkCategory + '&linkCode=' + linkCode);
  }

  onSaveContact(data: any): Observable<any> {
    return this.post('/api/contact/savecontact', data);
  }

  getDataService(langid: string): Observable<any> {
    return this.get(this.url +'/api/values/GetServiceData?langId=' + langid);
  }
  getServiceDetailData(langid: string, linkCode: string): Observable<any> {
    return this.get(this.url +'/api/values/GetServiceDetailData?langId=' + langid + '&linkCode=' + linkCode);
  }

  getMenuData(langid: string): Observable<any> {
    return this.get(this.url + '/api/values/GetMenuData?langId=' + langid);
  }

  /**
   * Order
  
   */
  getOrder(): Observable<any> {
    return this.get(this.url + '/api/values/GetDataOrder');
  }
  saveOrder(data: any): Observable<any> {
    return this.post(this.url + '/api/values/SaveOrder', data);
  }

}
