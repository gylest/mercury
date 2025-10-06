import { NgModule }         from '@angular/core';
import { OrderService }     from '../services/order.service';
import { DataModelOrders}   from './data-model-orders.model';

@NgModule({
  imports: [],
  providers: [DataModelOrders, OrderService]
})
export class DataModelOrdersModule { }
