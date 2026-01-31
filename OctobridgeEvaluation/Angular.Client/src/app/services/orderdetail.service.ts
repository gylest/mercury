import { inject, Injectable } from '@angular/core';
import { HttpClient }      from '@angular/common/http';
import { HttpHeaders }     from '@angular/common/http';
import { Observable }      from 'rxjs';
import { OrderDetail }     from '../models/orderdetail';

@Injectable({
  providedIn: 'root',
})
export class OrderDetailService {
  private readonly http = inject(HttpClient);

  //
  // Get order details by id
  //
  getById(id: number): Observable<OrderDetail[]>{

    const headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');

    // Get order details
    return this.http.get<OrderDetail[]>('api/OrderDetails/' + id, { headers });
  }
}
