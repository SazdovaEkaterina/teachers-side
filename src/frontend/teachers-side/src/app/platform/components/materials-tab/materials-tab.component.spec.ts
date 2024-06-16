import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MaterialsTabComponent } from './materials-tab.component';

describe('MaterialsTabComponent', () => {
  let component: MaterialsTabComponent;
  let fixture: ComponentFixture<MaterialsTabComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MaterialsTabComponent]
    });
    fixture = TestBed.createComponent(MaterialsTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
