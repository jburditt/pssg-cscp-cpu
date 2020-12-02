import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgramAuthorizerComponent } from './program-authorizer.component';

describe('ProgramAuthorizerComponent', () => {
  let component: ProgramAuthorizerComponent;
  let fixture: ComponentFixture<ProgramAuthorizerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProgramAuthorizerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgramAuthorizerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
