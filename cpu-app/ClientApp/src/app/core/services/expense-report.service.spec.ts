import { TestBed } from '@angular/core/testing';

import { ExpenseReportService } from './expense-report.service';

describe('ExpenseReportService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ExpenseReportService = TestBed.get(ExpenseReportService);
    expect(service).toBeTruthy();
  });
});
