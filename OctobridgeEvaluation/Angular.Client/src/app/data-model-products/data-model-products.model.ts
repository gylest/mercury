import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product';
import { CodedValue } from '../models/codedvalue';
import { ProductService } from '../services/product.service';
import { CodedValueService } from '../services/codedvalue.service';

//
// Types
//
export enum ProductStateEnum { Undefined = 1, Add, Delete, Edit }

@Injectable({
  providedIn: 'root'
})
export class DataModelProducts {
  //
  // Data Members
  //
  product: Product = new Product();
  heading = '';
  state: ProductStateEnum = ProductStateEnum.Undefined;
  ProductStatusValues: CodedValue[] = [];

  // Use inject() instead of constructor parameter injection
  private productService = inject(ProductService);
  private codedValueService = inject(CodedValueService);

  //
  // Constructor
  //
  constructor() {
    this.codedValueService.get('ProductStatus').subscribe(
      data => {
        this.ProductStatusValues = data;
      });
  }

  //
  // Methods
  //
  getProducts(): Observable<Product[]> {
    console.log(`Get Products`);
    return this.productService.get();
  }

  getProductById(id: number): Observable<Product> {
    console.log(`Get Product for ${id}`);
    return this.productService.getById(id);
  }

  updateProduct(product: Product): Observable<Product> {
    console.log(`Update Product: ${product.productId}`);
    return this.productService.update(product);
  }

  deleteProduct(product: Product): Observable<ArrayBuffer> {
    console.log(`Delete Product: ${product.productId}`);
    return this.productService.delete(product.productId);
  }

  addProduct(product: Product): Observable<Product> {
    return this.productService.add(product);
  }

  getStatusValues(): CodedValue[] {
    return this.ProductStatusValues;
  }

  //
  // Properties
  //
  getHeading(): string {
    return this.heading;
  }

  getProduct(): Product {
    return this.product;
  }

  setProduct(theProduct: Product) {
    this.product = theProduct;
  }

  getState(): ProductStateEnum {
    return this.state;
  }

  setState(theState: ProductStateEnum) {
    this.state = theState;

    switch (this.state) {
      case ProductStateEnum.Add: { this.heading = 'Add Product'; break; }
      case ProductStateEnum.Delete: { this.heading = 'Delete Product'; break; }
      case ProductStateEnum.Edit: { this.heading = 'Edit Product'; break; }
      default: { break; }
    }
  }

}
