import { Component, OnInit, inject } from '@angular/core';
import { FormGroup, NonNullableFormBuilder, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { DatePipe } from '@angular/common';
import { DataModelOrders, OrderStateEnum } from '../data-model-orders/data-model-orders.model';
import { DialogConfirmComponent, DialogConfirmModel } from '../dialog-confirm/dialog-confirm.component';
import { CodedValue } from '../models/codedvalue';
import { OrderDetail } from '../models/orderdetail';

@Component({
  selector: 'app-edit-order',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  styleUrls: ['./edit-order.component.css'],
  templateUrl: './edit-order.component.html'
})
export class EditOrderComponent implements OnInit {

  editForm!: FormGroup;
  submitted = false;
  hideFields = false;
  readonlyFields = false;

  displayedColumns: string[] = ['productId', 'unitPrice', 'quantity'];
  dataSource = new MatTableDataSource<OrderDetail>();

  private fb = inject(NonNullableFormBuilder);
  private model = inject(DataModelOrders);
  private router = inject(Router);
  private dialog = inject(MatDialog);

  ngOnInit() {
    // Locals
    const datePipe = new DatePipe('en-GB');

    // Set hidden fields value and read only fields value
    this.hideFields = this.model.getState() === OrderStateEnum.Add;
    this.readonlyFields = this.model.getState() === OrderStateEnum.View;

    // Initialize Reactive Form
    this.editForm = this.fb.group({
      order: this.fb.group({
        id: [''],
        orderStatus: [''],
        customerId: [''],
        freightAmount: [''],
        subTotal: [''],
        totalDue: [''],
        paymentDate: [''],
        shippedDate: [''],
        cancelDate: [''],
        recordCreated: [''],
        recordModified: ['']
      }),
      display: this.fb.group({
        recordModifiedString: ['']
      })
    });

    // Fill order form
    this.editForm.get('order')?.setValue(this.model.getOrder());

    // Fill display form
    const display = { recordModifiedString: datePipe.transform(this.model.getOrder().recordModified, 'dd/MM/yyyy HH:mm:ss') };
    this.editForm.get('display')?.setValue(display);

    // Fill datasource with data
    this.model.getOrderDetailsById(this.model.getOrder().id).subscribe(
      data => {
        this.dataSource = new MatTableDataSource(data);
      });

    // logging
    console.log('edit-order ngOnInit()');
  }

  getHeading(): string {
    return this.model.getHeading();
  }

  statusValues(): CodedValue[] {
    return this.model.getStatusValues();
  }

  cancel() {
    this.router.navigate(['manage-orders']);
  }

  onSubmit() {
    this.submitted = true;

    if (this.editForm.invalid) {
      return;
    }

    switch (this.model.getState()) {
      case OrderStateEnum.Add:
        {
          this.model.addOrder(this.editForm.get('order')?.value ?? {}).subscribe(
            data => {
              this.model.setOrder(data);
              this.router.navigate(['manage-orders']);
            }
          );
          break;
        }
      case OrderStateEnum.Delete:
        {
          const message = 'Are you sure you want to delete this order?';
          const dialogData = new DialogConfirmModel('Confirm Action', message);
          const dialogRef = this.dialog.open(DialogConfirmComponent, { maxWidth: '400px', data: dialogData });

          dialogRef.afterClosed().subscribe(dialogResult => {
            const dialogValue: boolean = dialogResult;
            if (dialogValue) {
              this.model.deleteOrder(this.editForm.get('order')?.value ?? {}).subscribe(
                () => {this.router.navigate(['manage-orders']);}
              );
            }
          });
          break;
        }
      case OrderStateEnum.Edit:
        {
          this.model.updateOrder(this.editForm.get('order')?.value ?? {}).subscribe(
            data => {
              this.model.setOrder(data);
              this.router.navigate(['manage-orders']);
            }
          );
          break;
        }
      default: { break; }
    }

  }

}
