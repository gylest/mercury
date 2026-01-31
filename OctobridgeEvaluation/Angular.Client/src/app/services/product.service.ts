import { Injectable, inject } from '@angular/core';
import { HttpClient }         from '@angular/common/http';
import { HttpHeaders }        from '@angular/common/http';
import { Observable }         from 'rxjs';
import { Product }            from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  //
  // Note: Base URL is added in HTTP Interceptor
  //
  private http = inject(HttpClient);

  //
  // Add a product
  //
  add(product: Product): Observable<Product>{
    const headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');
    const content = JSON.stringify(product);

    // Post product
    return this.http.post<Product>('api/Products', content, { headers });
  }

  //
  // Update a product
  //
  update(product: Product): Observable<Product>{
    const headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');
    const content = JSON.stringify(product);

    // Put product
    return this.http.put<Product>('api/Products/' + product.productId, content, { headers });
  }

  //
  // Delete a product
  //
  delete(id: number): Observable<ArrayBuffer>{
    const headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');

    // Delete product
    return this.http.delete<ArrayBuffer>('api/Products/' + id, { headers });
  }

  //
  // Get products
  //
  get(): Observable<Product[]>{
    const headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');

    // Get products
    return this.http.get<Product[]>('api/Products', { headers });
  }

  //
  // Get product by id
  //
  getById(id: number): Observable<Product> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');
    return this.http.get<Product>('api/Products/' + id, { headers});
  }
}
