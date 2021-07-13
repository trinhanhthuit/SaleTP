import { Observable } from 'rxjs';
/* eslint-disable prefer-arrow/prefer-arrow-functions */
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable({
	providedIn: 'root'
})
export class LocalStorageService extends BaseService {

	getByKey(key: string): any {
		return localStorage.getItem(key);
	}

	setByKey(key: string, data: string): any {
		localStorage.setItem(key, data);
	}

	clearByKey(key: string) {
		localStorage.removeItem(key);
	}
}

