import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgramContactComponent } from './program-contact.component';

describe('ProgramContactComponent', () => {
  let component: ProgramContactComponent;
  let fixture: ComponentFixture<ProgramContactComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProgramContactComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgramContactComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
