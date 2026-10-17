import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { provideNoopAnimations } from '@angular/platform-browser/animations';
import { provideRouter } from '@angular/router';
import { ManageCustomersComponent } from './manage-customers.component';

describe('ManageCustomersComponent', () => {
  let component: ManageCustomersComponent;
  let fixture: ComponentFixture<ManageCustomersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManageCustomersComponent],
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        provideNoopAnimations(),
        provideRouter([])
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ManageCustomersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
