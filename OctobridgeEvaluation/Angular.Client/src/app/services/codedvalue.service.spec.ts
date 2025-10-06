import { TestBed }                 from '@angular/core/testing';
import { CodedValueService }       from './codedvalue.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('CodedValueService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientTestingModule]
  }));

  it('should be created', () => {
      const service: CodedValueService = TestBed.inject(CodedValueService);
    expect(service).toBeTruthy();
  });
});
