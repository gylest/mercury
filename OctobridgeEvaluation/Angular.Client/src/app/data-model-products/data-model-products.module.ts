import { NgModule }         from '@angular/core';
import { ProductService }   from '../services/product.service';
import { DataModelProducts} from './data-model-products.model';

@NgModule({
  imports: [],
  providers: [DataModelProducts, ProductService]
})
export class DataModelProductsModule { }
