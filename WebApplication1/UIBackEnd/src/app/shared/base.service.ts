import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable()
export class BaseService {

  constructor(protected http: HttpClient) {
  }

  protected get<Type>(url: string, data?: Type): Observable<Type> {
    return this.http.get<Type>(url, data).pipe(retry(2), catchError(this.handleError));
  }

  // eslint-disable-next-line @typescript-eslint/ban-types
  protected post(url: string, data?: any, options?: any): Observable<Object> {
    return this.http.post<any>(url, data, options).pipe(retry(2), catchError(this.handleError));
  }

  protected put<Type>(url: string, data: Type) {
    return this.http.put(url, data)
      .pipe(
        retry(2),
        catchError(this.handleError)
      );
  }

  protected delete<Type>(url: string, data?: Type) {
    return this.http.request('delete', url, { body: data })
      .pipe(
        retry(2),
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // Return an observable with a user-facing error message.
    return throwError('Something bad happened; please try again later.');
  }
  private dateToYMD(date: Date) {
    var d = date.getDate();
    var m = date.getMonth() + 1; //Month from 0 to 11
    var y = date.getFullYear();
    return '' + y + '-' + (m <= 9 ? '0' + m : m) + '-' + (d <= 9 ? '0' + d : d);
  }

}
