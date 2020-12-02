import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonPickerComponent } from './person-picker.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

describe('PersonPickerComponent', () => {
  let component: PersonPickerComponent;
  let fixture: ComponentFixture<PersonPickerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [PersonPickerComponent],
      imports: [FormsModule, ReactiveFormsModule]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PersonPickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
