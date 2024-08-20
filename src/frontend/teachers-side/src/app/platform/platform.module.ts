import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTabsModule } from '@angular/material/tabs';
import { MatCardModule } from '@angular/material/card'; 
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { HomeComponent } from './components/home/home.component';
import { EventsTabComponent } from './components/events-tab/events-tab.component';
import { MaterialsTabComponent } from './components/materials-tab/materials-tab.component';
import { ForumsTabComponent } from './components/forums-tab/forums-tab.component';
import { EventCardComponent } from './components/event-card/event-card.component';
import { AddEditEventComponent } from './components/add-edit-event/add-edit-event.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    HomeComponent,
    EventsTabComponent,
    MaterialsTabComponent,
    ForumsTabComponent,
    EventCardComponent,
    AddEditEventComponent,
  ],
  imports: [
    CommonModule,
    MatTabsModule,
    MatCardModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
  ]
})
export class PlatformModule { }
