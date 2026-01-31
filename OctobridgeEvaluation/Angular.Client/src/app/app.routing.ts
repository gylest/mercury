import { Routes }                     from '@angular/router';
import { HomeComponent }              from './home/home.component';
import { ManageCustomersComponent }   from './manage-customers/manage-customers.component';
import { ManageOrdersComponent }      from './manage-orders/manage-orders.component';
import { ManageAttachmentsComponent } from './manage-attachments/manage-attachments.component';
import { ManageProductsComponent }    from './manage-products/manage-products.component';
import { EditOrderComponent }         from './edit-order/edit-order.component';
import { EditCustomerComponent }      from './edit-customer/edit-customer.component';
import { EditProductComponent }       from './edit-product/edit-product.component';

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'manage-attachments', component: ManageAttachmentsComponent },
  { path: 'manage-customers', component: ManageCustomersComponent },
  { path: 'manage-orders', component: ManageOrdersComponent },
  { path: 'manage-products', component: ManageProductsComponent },
  { path: 'edit-order', component: EditOrderComponent },
  { path: 'edit-customer', component: EditCustomerComponent },
  { path: 'edit-product', component: EditProductComponent },
  { path: '**', component: HomeComponent}
];
