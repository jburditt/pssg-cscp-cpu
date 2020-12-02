import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CgLiabilityComponent } from './cg-liability.component';

describe('CgLiabilityComponent', () => {
  let component: CgLiabilityComponent;
  let fixture: ComponentFixture<CgLiabilityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CgLiabilityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CgLiabilityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
