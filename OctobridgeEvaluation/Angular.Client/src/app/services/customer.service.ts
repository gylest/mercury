import { Injectable, inject }      from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable }              from 'rxjs';
import { Customer }                from '../models/customer';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  //
  // Note: Base URL is added in HTTP Interceptor
  //
  private http = inject(HttpClient);

  //
  // Add a customer
  //
  add(customer: Customer): Observable<Customer> {

    const headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');
    const content = JSON.stringify(customer);

    // Post customer
    return this.http.post<Customer>('api/Customers', content, { headers});
  }

  //
  // Update a customer
  //
  update(customer: Customer): Observable<Customer> {

    const headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');
    const content = JSON.stringify(customer);

    // Put customer
    return this.http.put<Customer>('api/Customers/' + customer.id, content, { headers });
  }

  //
  // Delete a customer
  //
  delete(id: number): Observable<ArrayBuffer> {

    const headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');

    // Delete customer
    return this.http.delete<ArrayBuffer>('api/Customers/' + id, { headers });
  }

  //
  // Get customers
  //
  get(): Observable<Customer[]>{

    const headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');

    // Get customers
    return this.http.get<Customer[]>('api/Customers', { headers });
  }
}
