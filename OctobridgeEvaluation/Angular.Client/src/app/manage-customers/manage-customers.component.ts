import { Component, OnInit, AfterViewInit, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { DataModelCustomers, CustomerStateEnum } from '../data-model-customers/data-model-customers.model';
import { Customer } from '../models/customer';

@Component({
  selector: 'app-manage-customers',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatCardModule,
  ],
  styleUrls: ['./manage-customers.component.css'],
  templateUrl: './manage-customers.component.html'
})

export class ManageCustomersComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'middleName', 'addressLine1', 'addressLine2', 'city', 'postalCode', 'telephone', 'email', 'recordModified', 'action'];

  dataSource = new MatTableDataSource<Customer>();

  private model = inject(DataModelCustomers);
  private router = inject(Router);

  ngOnInit() {
    this.model.getCustomers().subscribe(
      data => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  editCustomer(element: Customer): void {
    this.model.setCustomer(element);
    this.model.setState(CustomerStateEnum.Edit);
    this.router.navigate(['edit-customer']);
  }

  deleteCustomer(element: Customer): void {
    this.model.setCustomer(element);
    this.model.setState(CustomerStateEnum.Delete);
    this.router.navigate(['edit-customer']);
  }

  addCustomer(): void {
    const customer = new Customer();
    this.model.setCustomer(customer);
    this.model.setState(CustomerStateEnum.Add);
    this.router.navigate(['edit-customer']);
  }

  /**
* Helper function to format column keys into readable headers.
*/
  formatHeader(columnKey: string): string {
    if (columnKey === 'action') {
      return 'Action';
    }
    // Convert camelCase to title case (e.g., 'productNumber' to 'PRODUCT NUMBER')
    return columnKey
      .replace(/[A-Z]/g, letter => ' ' + letter)
      .toUpperCase();
  }

}
