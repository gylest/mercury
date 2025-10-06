import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { DataModelProducts, ProductStateEnum } from '../data-model-products/data-model-products.model';
import { Product } from '../models/product';

@Component({
  selector: 'app-manage-products',
  standalone: true,
  imports: [
    CommonModule,
    MatPaginator,
    MatSort,
    MatTableModule,
    MatCardModule
  ],
  styleUrls: ['./manage-products.component.css'],
  templateUrl: './manage-products.component.html'
})
export class ManageProductsComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  displayedColumns: string[] = ['productId', 'name', 'productNumber', 'productCategoryId', 'cost', 'recordCreated', 'recordModified', 'action'];

  dataSource = new MatTableDataSource<Product>();

  constructor(private model: DataModelProducts, private router: Router) { }

  ngOnInit() {
    this.model.getProducts().subscribe(
      data => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  editProduct(element: Product): void {
    this.model.setProduct(element);
    this.model.setState(ProductStateEnum.Edit);
    this.router.navigate(['edit-product']);
  }

  deleteProduct(element: Product): void {
    this.model.setProduct(element);
    this.model.setState(ProductStateEnum.Delete);
    this.router.navigate(['edit-product']);
  }

  addProduct(): void {
    const product = new Product();
    this.model.setProduct(product);
    this.model.setState(ProductStateEnum.Add);
    this.router.navigate(['edit-product']);
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
