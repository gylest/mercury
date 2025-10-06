import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { DataModelOrders, OrderStateEnum } from '../data-model-orders/data-model-orders.model';
import { Order } from '../models/order';

@Component({
  selector: 'app-manage-orders',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginator,
    MatSort,
    MatCardModule,
  ],
  styleUrls: ['./manage-orders.component.css'],
  templateUrl: './manage-orders.component.html'
})
export class ManageOrdersComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  displayedColumns: string[] = ['id', 'orderStatus', 'customerId', 'freightAmount', 'subTotal', 'totalDue', 'paymentDate', 'shippedDate', 'cancelDate', 'recordCreated', 'recordModified', 'action'];
  dataSource = new MatTableDataSource<Order>();

  constructor(private model: DataModelOrders, private router: Router) { }

  ngOnInit() {
    this.model.getOrders().subscribe(
      data => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  viewOrder(element: Order): void {
    this.model.setOrder(element);
    this.model.setState(OrderStateEnum.View);
    this.router.navigate(['edit-order']);
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

};
