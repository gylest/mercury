import { Injectable }      from '@angular/core';
import { HttpClient }      from '@angular/common/http';
import { HttpHeaders }     from '@angular/common/http';
import { Observable }      from 'rxjs';
import { Order }           from '../models/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  //
  // Note: Base URL is added in HTTP Interceptor
  //
  constructor(private http: HttpClient) { }

  //
  // Add an order
  //
  add(order: Order): Observable<Order>{

    const headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');
    const content = JSON.stringify(order);

    // Post order
    return this.http.post<Order>('api/Orders', content, { headers });
  }

  //
  // Update an order
  //
  update(order: Order): Observable<Order>{

    const headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');
    const content = JSON.stringify(order);

    // Put order
    return this.http.put<Order>('api/Orders/' + order.id, content, { headers });
  }

  //
  // Delete an order
  //
  delete(id: number): Observable<ArrayBuffer>{

    const headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');

    // Delete order
    return this.http.delete<ArrayBuffer>('api/Orders/' + id, { headers });
  }

  //
  // Get orders
  //
  get(): Observable<Order[]>{

    const headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');

    // Get orders
    return this.http.get<Order[]>('api/Orders', { headers });
  }
}
