import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewUserNewOrganizationComponent } from './new-user-new-organization.component';

describe('NewUserNewOrganizationComponent', () => {
  let component: NewUserNewOrganizationComponent;
  let fixture: ComponentFixture<NewUserNewOrganizationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewUserNewOrganizationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewUserNewOrganizationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
