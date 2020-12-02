import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgramBudgetComponent } from './program-budget.component';

describe('ProgramBudgetComponent', () => {
  let component: ProgramBudgetComponent;
  let fixture: ComponentFixture<ProgramBudgetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProgramBudgetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgramBudgetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
