import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgramContactInformationComponent } from './program-contact-information.component';

describe('ProgramContactInformationComponent', () => {
  let component: ProgramContactInformationComponent;
  let fixture: ComponentFixture<ProgramContactInformationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProgramContactInformationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgramContactInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
