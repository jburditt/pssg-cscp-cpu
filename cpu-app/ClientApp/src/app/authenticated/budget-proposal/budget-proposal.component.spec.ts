import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BudgetProposalComponent } from './budget-proposal.component';

describe('BudgetProposalComponent', () => {
  let component: BudgetProposalComponent;
  let fixture: ComponentFixture<BudgetProposalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BudgetProposalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BudgetProposalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
