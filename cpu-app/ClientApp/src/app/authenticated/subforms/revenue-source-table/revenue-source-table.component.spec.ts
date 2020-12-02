import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RevenueSourceTableComponent } from './revenue-source-table.component';

describe('RevenueSourceTableComponent', () => {
  let component: RevenueSourceTableComponent;
  let fixture: ComponentFixture<RevenueSourceTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RevenueSourceTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RevenueSourceTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
