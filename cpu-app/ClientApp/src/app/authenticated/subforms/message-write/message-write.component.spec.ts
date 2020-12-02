import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MessageWriteComponent } from './message-write.component';

describe('MessageWriteComponent', () => {
  let component: MessageWriteComponent;
  let fixture: ComponentFixture<MessageWriteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [MessageWriteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MessageWriteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
