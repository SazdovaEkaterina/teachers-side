import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventAddDialogComponent } from './event-add-dialog.component';

describe('EventAddDialogComponent', () => {
  let component: EventAddDialogComponent;
  let fixture: ComponentFixture<EventAddDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EventAddDialogComponent]
    });
    fixture = TestBed.createComponent(EventAddDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
