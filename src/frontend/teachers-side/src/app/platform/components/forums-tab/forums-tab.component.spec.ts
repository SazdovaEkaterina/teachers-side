import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForumsTabComponent } from './forums-tab.component';

describe('ForumsTabComponent', () => {
  let component: ForumsTabComponent;
  let fixture: ComponentFixture<ForumsTabComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ForumsTabComponent],
    });
    fixture = TestBed.createComponent(ForumsTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
