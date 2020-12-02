import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonnelExpenseTableComponent } from './personnel-expense-table.component';

describe('PersonnelExpenseTableComponent', () => {
  let component: PersonnelExpenseTableComponent;
  let fixture: ComponentFixture<PersonnelExpenseTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PersonnelExpenseTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PersonnelExpenseTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
