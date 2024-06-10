import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ValidationRulesComponent } from './validation-rules.component';

describe('ValidationRulesComponent', () => {
  let component: ValidationRulesComponent;
  let fixture: ComponentFixture<ValidationRulesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ValidationRulesComponent]
    });
    fixture = TestBed.createComponent(ValidationRulesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
