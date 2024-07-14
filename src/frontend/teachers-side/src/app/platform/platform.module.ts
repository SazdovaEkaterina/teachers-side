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
import { EventAddDialogComponent } from './components/event-add-dialog/event-add-dialog.component';
import { EventDetailsDialogComponent } from './components/event-details-dialog/event-details-dialog.component';

@NgModule({
  declarations: [
    HomeComponent,
    EventsTabComponent,
    MaterialsTabComponent,
    ForumsTabComponent,
    EventCardComponent,
    EventAddDialogComponent,
    EventDetailsDialogComponent
  ],
  imports: [
    CommonModule,
    MatTabsModule,
    MatCardModule,
    BrowserAnimationsModule
  ]
})
export class PlatformModule { }
