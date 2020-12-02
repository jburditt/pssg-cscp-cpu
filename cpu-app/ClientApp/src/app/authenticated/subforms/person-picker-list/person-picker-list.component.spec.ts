import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonPickerListComponent } from './person-picker-list.component';

describe('PersonPickerListComponent', () => {
  let component: PersonPickerListComponent;
  let fixture: ComponentFixture<PersonPickerListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PersonPickerListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PersonPickerListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
