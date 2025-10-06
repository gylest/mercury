import { NgModule }            from '@angular/core';
import { CustomerService }     from '../services/customer.service';
import { DataModelCustomers}   from './data-model-customers.model';

@NgModule({
  imports: [],
  providers: [DataModelCustomers, CustomerService]
})
export class DataModelCustomersModule { }
