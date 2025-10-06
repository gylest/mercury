import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { FormGroup, NonNullableFormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { saveAs } from 'file-saver';
import { Attachment } from '../models/attachment';
import { AttachmentService } from '../services/attachment.service';
import { DialogConfirmComponent, DialogConfirmModel } from '../dialog-confirm/dialog-confirm.component';

@Component({
  selector: 'app-manage-attachments',
  standalone: true,
  imports: [MatTableModule, MatPaginator, MatSort, CommonModule, ReactiveFormsModule, MatCardModule],
  styleUrls: ['./manage-attachments.component.css'],
  templateUrl: './manage-attachments.component.html',
})
export class ManageAttachmentsComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  displayedColumns: string[] = ['id', 'fileName', 'fileType', 'length', 'recordCreated', 'action'];
  public progress!: number;
  public message!: string;
  form!: FormGroup;
  error!: string;
  uploadResponse = { status: '', message: '', filePath: '' };
  dataSource = new MatTableDataSource<Attachment>();

  constructor(private fb: NonNullableFormBuilder, private attachmentService: AttachmentService, private dialog: MatDialog) { }

  ngOnInit() {
    this.form = this.fb.group({
      avatar: ['']
    });

    this.attachmentService.get('').subscribe(
      data => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  onFileChange(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      this.form.get('avatar')?.setValue(file);
    }
  }

  onSubmit() {
    const formData = new FormData();
    formData.append('file', this.form.get('avatar')?.value ?? {});

    this.attachmentService.addFile(formData).subscribe(
      (res) => {
        this.uploadResponse = res;

        if (this.uploadResponse.status === 'response') {
          // log
          console.log(`File upload completed`);
          // create new attachment, add to data and refresh
          const attachment = new Attachment(res.body.id, res.body.fileName, res.body.fileType, res.body.length, res.body.createDate);
          this.dataSource.data.push(attachment);
          this.dataSource._updateChangeSubscription();
        }
      },
      (err) => this.error = err
    );
  }

  downloadFile(element: Attachment): void {

    this.attachmentService.getFile(element.id)
      .subscribe(
        (data: Blob) => {
          saveAs(data, element.fileName);
        },
        (err: any) => {
          console.log(`Unable to download file ${JSON.stringify(err)}`);
        }
      );
  }

  deleteFile(element: Attachment): void {

    const message = 'Are you sure you want to delete this attachment?';
    const dialogData = new DialogConfirmModel('Confirm Action', message);
    const dialogRef = this.dialog.open(DialogConfirmComponent, { maxWidth: '400px', data: dialogData });

    dialogRef.afterClosed().subscribe(dialogResult => {
      const dialogValue: boolean = dialogResult;
      if (dialogValue) {
        this.attachmentService.delete(element.id)
          .subscribe(
            (data) => {
              // log
              console.log(`File deleted ${JSON.stringify(element.fileName)}`);
              // Find index of deleted attachment, remove from data and refresh
              const index = this.dataSource.data.findIndex(({ id }) => id === element.id);
              this.dataSource.data.splice(index, 1);
              this.dataSource._updateChangeSubscription(); //refresh the dataSource
            },
            (err: any) => {
              console.log(`Unable to delete file ${JSON.stringify(err)}`);
            }
          );
      }
    });
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
