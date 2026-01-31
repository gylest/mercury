import { Injectable, inject }                                from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders, HttpEventType} from '@angular/common/http';
import { Observable }                                        from 'rxjs';
import { map }                                               from 'rxjs/operators';
import { Attachment }                                        from '../models/attachment';

@Injectable({
  providedIn: 'root'
})
export class AttachmentService {
  //
  // Note: Base URL is added in HTTP Interceptor
  //

  private http = inject(HttpClient);

  //
  // Upload an Attachment
  //
  addFile(formData: FormData): Observable<any>{

    return this.http.post('api/Attachments', formData, { reportProgress: true, observe: 'events' }).pipe(
      map((event) => {
        switch (event.type) {
          case HttpEventType.UploadProgress:
            if (event.total && event.total > 0) {
              const progress = Math.round(100 * event.loaded / event.total);
              console.log(`Post of Attachment in progress: ${progress}`);
              return { status: 'progress', message: progress };
            } else {
              // Fallback if total is not available
              const progress = 0;
              return { status: 'progress', message: progress };
            }
          case HttpEventType.Response:
            console.log(`Post of Attachment completed`);
            console.log(`Event Body = ${JSON.stringify(event.body)}`);
            return { status: 'response', message: 100, body: event.body };
          default:
            return { status: 'unhandled', event };
        }
      })
    );
  }

  //
  // Delete an attachment
  //
  delete(id: number): Observable<ArrayBuffer>{

    const headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');

    // Send delete command to Rest Server
    return this.http.delete<ArrayBuffer>('api/Attachments/' + id, { headers });
  }

  //
  // Download Attachment
  //
  getFile(id: number): Observable <Blob>{

    return this.http.get('api/Attachments/' + id, { responseType: 'blob' });
  }

  //
  // Get Attachments by filename
  //
  get(fileName: string): Observable<Attachment[]> {

    const params  = new HttpParams().set('fileName', fileName);
    const headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');

    return this.http.get<Attachment[]>('api/Attachments', { headers, params});

  }
}
