import { Injectable, inject } from '@angular/core';
import { Observable }         from 'rxjs';
import { Order }              from '../models/order';
import { OrderDetail }        from '../models/orderdetail';
import { CodedValue }         from '../models/codedvalue';
import { OrderService }       from '../services/order.service';
import { OrderDetailService } from '../services/orderdetail.service';
import { CodedValueService }  from '../services/codedvalue.service';

//
// Types
//
export enum OrderStateEnum { Undefined = 1, Add, Delete, Edit, View}

@Injectable({
  providedIn: 'root'
})
export class DataModelOrders {
  //
  // Data Members
  //
  order: Order = new Order();
  heading = '';
  state: OrderStateEnum = OrderStateEnum.Undefined;
  orderStatusValues: CodedValue[] = [];

  //
  // Services (using inject)
  //
  private orderService = inject(OrderService);
  private orderDetailService = inject(OrderDetailService);
  private codedValueService = inject(CodedValueService);

  //
  // Constructor
  //
  constructor() {
    this.codedValueService.get('OrderStatus').subscribe(
      data => {
        this.orderStatusValues = data;
      });
  }

  //
  // Methods
  //

  getOrders(): Observable<Order[]> {
    console.log(`Get orders`);
    return this.orderService.get();
  }

  updateOrder(order: Order): Observable<Order>{
    console.log(`Update order: ${order.id}`);
    return this.orderService.update(order);
  }

  deleteOrder(order: Order): Observable<ArrayBuffer>{
    console.log(`Delete order: ${order.id}`);
    return this.orderService.delete(order.id);
  }

  addOrder(order: Order): Observable<Order> {
    return this.orderService.add(order);
  }

  getStatusValues(): CodedValue[] {
    return this.orderStatusValues;
  }

  getOrderDetailsById(id: number): Observable<OrderDetail[]> {
    console.log(`Get orderdetails for ${id}`);
    return this.orderDetailService.getById(id);
  }

  //
  // Properties
  //
  getHeading(): string {
    return this.heading;
  }

  getOrder(): Order {
    return this.order;
  }

  setOrder(pOrder: Order) {
    this.order = pOrder;
  }

  getState(): OrderStateEnum {
    return this.state;
  }

  setState(pState: OrderStateEnum) {
    this.state = pState;

    switch (this.state) {
      case OrderStateEnum.View: { this.heading = 'View Order'; break; }
      default: { break; }
    }
  }

}
