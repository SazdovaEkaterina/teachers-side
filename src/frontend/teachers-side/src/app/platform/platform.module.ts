import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTabsModule } from '@angular/material/tabs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { HomeComponent } from './components/home/home.component';
import { EventsTabComponent } from './components/events-tab/events-tab.component';
import { MaterialsTabComponent } from './components/materials-tab/materials-tab.component';
import { ForumsTabComponent } from './components/forums-tab/forums-tab.component';

@NgModule({
  declarations: [
    HomeComponent,
    EventsTabComponent,
    MaterialsTabComponent,
    ForumsTabComponent
  ],
  imports: [
    CommonModule,
    MatTabsModule,
    BrowserAnimationsModule
  ]
})
export class PlatformModule { }
