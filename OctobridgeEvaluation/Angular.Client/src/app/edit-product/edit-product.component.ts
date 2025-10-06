import { Component, OnInit } from '@angular/core';
import { FormGroup, NonNullableFormBuilder, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { DatePipe } from '@angular/common';
import { DataModelProducts, ProductStateEnum } from '../data-model-products/data-model-products.model';
import { DialogConfirmComponent, DialogConfirmModel } from '../dialog-confirm/dialog-confirm.component';
import { CodedValue } from '../models/codedvalue';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-edit-product',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule 
  ],
  styleUrls: ['./edit-product.component.css'],
  templateUrl: './edit-product.component.html'
})
export class EditProductComponent implements OnInit {

  editForm!: FormGroup;
  submitted = false;
  hideFields = false;
  readonlyFields = false;

  constructor(private fb: NonNullableFormBuilder, private model: DataModelProducts, private router: Router, private dialog: MatDialog) { }

  ngOnInit() {
    // Locals
    const datePipe = new DatePipe('en-GB');

    // Set hidden fields value and read only fields value
    this.hideFields = this.model.getState() === ProductStateEnum.Add;
    this.readonlyFields = this.model.getState() === ProductStateEnum.Delete;

    // Initialize Reactive Form
    this.editForm = this.fb.group({
      product: this.fb.group({
        productId: [''],
        name: [''],
        productNumber: [''],
        productCategoryId: [''],
        cost: [''],
        recordCreated: [''],
        recordModified: ['']
      }),
      display: this.fb.group({
        recordModifiedString: ['']
      })
    });

    // Fill product form
    this.editForm.get('product')!.setValue(this.model.getProduct());

    // Fill display form
    const display = { recordModifiedString: datePipe.transform(this.model.getProduct().recordModified, 'dd/MM/yyyy HH:mm:ss') };
    this.editForm.get('display')!.setValue(display);

    // logging
    console.log('edit-product ngOnInit()');
  }

  getHeading(): string {
    return this.model.getHeading();
  }

  statusValues(): CodedValue[] {
    return this.model.getStatusValues();
  }

  cancel() {
    this.router.navigate(['manage-products']);
  }

  onSubmit() {
    this.submitted = true;

    if (this.editForm.invalid) {
      return;
    }

    switch (this.model.getState()) {
      case ProductStateEnum.Add:
        {
          this.model.addProduct(this.editForm.get('product')?.value ?? {}).subscribe(
            data => {
              this.model.setProduct(data);
              this.router.navigate(['manage-products']);
            }
          );
          break;
        }
      case ProductStateEnum.Delete:
        {
          const message = 'Are you sure you want to delete this product?';
          const dialogData = new DialogConfirmModel('Confirm Action', message);
          const dialogRef = this.dialog.open(DialogConfirmComponent, { maxWidth: '400px', data: dialogData });

          dialogRef.afterClosed().subscribe(dialogResult => {
            const dialogValue: boolean = dialogResult;
            if (dialogValue) {
              this.model.deleteProduct(this.editForm.get('product')?.value ?? {}).subscribe(
                data => {
                  this.router.navigate(['manage-products']);
                }
              );
            }
          });
          break;
        }
      case ProductStateEnum.Edit:
        {
          this.model.updateProduct(this.editForm.get('product')?.value ?? {}).subscribe(
            data => {
              this.model.setProduct(data);
              this.router.navigate(['manage-products']);
            }
          );
          break;
        }
      default: { break; }
    }

  }

}
