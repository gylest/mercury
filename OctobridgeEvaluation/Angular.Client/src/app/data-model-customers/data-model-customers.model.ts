import { Injectable }      from '@angular/core';
import { Observable }      from 'rxjs';
import { Customer }        from '../models/customer';
import { CustomerService } from '../services/customer.service';

//
// Types
//
export enum CustomerStateEnum { Undefined = 1, Add, Delete, Edit }

@Injectable({
  providedIn: 'root'
})
export class DataModelCustomers {
  //
  // Data Members
  //
  customer: Customer = new Customer();
  heading: string = '';
  state: CustomerStateEnum = CustomerStateEnum.Undefined;

  //
  // Constructor
  //
  constructor(private customerService: CustomerService) {}

  //
  // Methods
  //
  getCustomers(): Observable<Customer[]> {
    console.log(`Get customers`);
    return this.customerService.get();
  }

  updateCustomer(customer: Customer): Observable<Customer>{
    console.log(`Update customer: ${customer.id}`);
    return this.customerService.update(customer);
  }

  deleteCustomer(customer: Customer): Observable<ArrayBuffer>{
    console.log(`Delete customer: ${customer.id}`);
    return this.customerService.delete(customer.id);
  }

  addCustomer(customer: Customer): Observable<Customer> {
    return this.customerService.add(customer);
  }

  //
  // Properties
  //
  getHeading(): string {
    return this.heading;
  }

  getCustomer(): Customer {
    return this.customer;
  }

  setCustomer(customer: Customer) {
    this.customer = customer;
  }

  getState(): CustomerStateEnum {
    return this.state;
  }

  setState(pState: CustomerStateEnum) {
    this.state = pState;

    switch (this.state) {
      case CustomerStateEnum.Add: { this.heading = 'Add Customer'; break; }
      case CustomerStateEnum.Delete: { this.heading = 'Delete Customer'; break; }
      case CustomerStateEnum.Edit: { this.heading = 'Edit Customer'; break; }
      default: { break; }
    }
  }

}
