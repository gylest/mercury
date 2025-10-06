import { Injectable }                           from '@angular/core';
import { HttpClient, HttpParams, HttpEventType} from '@angular/common/http';
import { HttpHeaders }                          from '@angular/common/http';
import { Observable }                           from 'rxjs';
import { CodedValue }                           from '../models/codedvalue';

@Injectable({
  providedIn: 'root'
})
export class CodedValueService {
    //
    // Note: Base URL is added in HTTP Interceptor
    //
    constructor(private http: HttpClient) { }

    //
    // Get CodedValues by Group Name
    //
    get(groupName: string): Observable<CodedValue[]> {

      const params = new HttpParams().set('groupName', groupName);
      const headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');
      return this.http.get<CodedValue[]>('api/CodedValues', { headers, params });
  }
}


