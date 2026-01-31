import { Component, OnInit, inject }                     from '@angular/core';
import { FormGroup, NonNullableFormBuilder, Validators } from '@angular/forms';
import { ReactiveFormsModule }                           from '@angular/forms';
import { Router }                                        from '@angular/router';
import { MatDialog }                                     from '@angular/material/dialog';
import { MatFormFieldModule }                            from '@angular/material/form-field';
import { MatInputModule }                                from '@angular/material/input';
import { DatePipe }                                      from '@angular/common';
import { DataModelCustomers, CustomerStateEnum }         from '../data-model-customers/data-model-customers.model';
import { DialogConfirmComponent, DialogConfirmModel }    from '../dialog-confirm/dialog-confirm.component';
import { Customer }                                      from '../models/customer';

@Component({
  selector: 'app-edit-customer',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule
  ],
  styleUrls: ['./edit-customer.component.css'],
  templateUrl: './edit-customer.component.html'
})
export class EditCustomerComponent implements OnInit {

  editForm!: FormGroup;
  submitted = false;
  hideFields = false;
  readonlyFields = false;
  customer!: Customer;

  // Remove constructor and use inject() for dependencies
  private fb = inject(NonNullableFormBuilder);
  private model = inject(DataModelCustomers);
  private router = inject(Router);
  private dialog = inject(MatDialog);

  ngOnInit() {
    // Locals
    const datePipe = new DatePipe('en-GB');

    // Set hidden fields value and read only fields value
    this.hideFields = this.model.getState() === CustomerStateEnum.Add;
    this.readonlyFields = this.model.getState() === CustomerStateEnum.Delete;

    // Get customer
    this.customer = this.model.getCustomer();

    // Initialize Reactive Form
    this.editForm = this.fb.group({
      customer: this.fb.group({
        id: [''],
        firstName: [''],
        lastName: [''],
        middleName: [''],
        addressLine1: [''],
        addressLine2: [''],
        city: [''],
        postalCode: [''],
        telephone: [''],
        email: ['', Validators.email],
        recordCreated: [''],
        recordModified: [''],
      }),
      display: this.fb.group({
        recordModifiedString: ['']
      })
    });

    // Fill customer form
    this.editForm.get('customer')?.setValue(this.customer);

    // Fill display form
    const display = { recordModifiedString: datePipe.transform(this.customer.recordModified, 'dd/MM/yyyy HH:mm:ss') };
    this.editForm.get('display')?.setValue(display);

    // logging
    console.log(`edit-customer: First Name: ${this.customer.firstName}, Last Name: ${this.customer.lastName}`);
  }

  getHeading(): string {
    return this.model.getHeading();
  }

  cancel() {
    this.router.navigate(['manage-customers']);
  }

  onSubmit() {
    this.submitted = true;

    if (this.editForm.invalid) {
      return;
    }

    switch (this.model.getState()) {
      case CustomerStateEnum.Add:
        {
          this.model.addCustomer(this.editForm.get('customer')?.value ?? {}).subscribe(
            data => {
              this.model.setCustomer(data);
              this.router.navigate(['manage-customers']);
            }
          );
          break;
        }
      case CustomerStateEnum.Delete:
        {
          const message = 'Are you sure you want to delete this customer?';
          const dialogData = new DialogConfirmModel('Confirm Action', message);
          const dialogRef = this.dialog.open(DialogConfirmComponent, { maxWidth: '400px', data: dialogData });

          dialogRef.afterClosed().subscribe(dialogResult => {
            const dialogValue: boolean = dialogResult;
            if (dialogValue) {
              this.model.deleteCustomer(this.editForm.get('customer')?.value ?? {}).subscribe(
                () => { this.router.navigate(['manage-customers']);}
              );
            }
          });
          break;
        }
      case CustomerStateEnum.Edit:
        {
          this.model.updateCustomer(this.editForm.get('customer')?.value ?? {}).subscribe(
            data => {
              this.model.setCustomer(data);
              this.router.navigate(['manage-customers']);
            }
          );
          break;
        }
      default: { break; }
    }

  }

}
