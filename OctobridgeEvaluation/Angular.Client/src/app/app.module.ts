import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpErrorInterceptor } from './http-error.interceptor';
import { MaterialModule } from './material.module';
import { AppComponent } from './app.component';
import { MatToolbarComponent } from './mat-toolbar/mat-toolbar.component';
import { FooterComponent } from './footer/footer.component';
import { HomeComponent } from './home/home.component';
import { ManageOrdersComponent } from './manage-orders/manage-orders.component';
import { EditOrderComponent } from './edit-order/edit-order.component';
import { EditCustomerComponent } from './edit-customer/edit-customer.component';
import { EditProductComponent } from './edit-product/edit-product.component';
import { ManageCustomersComponent } from './manage-customers/manage-customers.component';
import { ManageAttachmentsComponent } from './manage-attachments/manage-attachments.component';
import { ManageProductsComponent } from './manage-products/manage-products.component';
import { DataModelOrdersModule } from './data-model-orders/data-model-orders.module';
import { DataModelCustomersModule } from './data-model-customers/data-model-customers.module';
import { DataModelProductsModule } from './data-model-products/data-model-products.module';
import { DialogConfirmComponent } from './dialog-confirm/dialog-confirm.component';
import { MAT_DATE_LOCALE } from '@angular/material/core';

@NgModule({
  declarations: [

  ],
  imports: [
    AppComponent,
    FooterComponent,
    MatToolbarComponent,
    HomeComponent,
    ManageOrdersComponent,
    ManageCustomersComponent,
    ManageAttachmentsComponent,
    ManageProductsComponent,
    EditOrderComponent,
    EditCustomerComponent,
    EditProductComponent,
    DialogConfirmComponent,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    DataModelOrdersModule,
    DataModelCustomersModule,
    DataModelProductsModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },
    {
      provide: MAT_DATE_LOCALE,
      useValue: 'en-GB'
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
