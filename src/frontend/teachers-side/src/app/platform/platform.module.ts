import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTabsModule } from '@angular/material/tabs';
import { MatCardModule } from '@angular/material/card'; 
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatExpansionModule } from '@angular/material/expansion';

import { HomeComponent } from './components/home/home.component';
import { EventsTabComponent } from './components/events-tab/events-tab.component';
import { MaterialsTabComponent } from './components/materials-tab/materials-tab.component';
import { ForumsTabComponent } from './components/forums-tab/forums-tab.component';
import { EventCardComponent } from './components/event-card/event-card.component';
import { AddEditEventComponent } from './components/add-edit-event/add-edit-event.component';
import { ForumPostCardComponent } from './components/forum-post-card/forum-post-card.component';
import { AddEditForumPostComponent } from './components/add-edit-forum-post/add-edit-forum-post.component';
import { MaterialCardComponent } from './components/material-card/material-card.component';
import { FileTypePipe } from './pipes/file-type.pipe';
import { SubjectCategoryPipe } from './pipes/subject-category.pipe';
import { AddEditMaterialComponent } from './components/add-edit-material/add-edit-material.component';
import { ForumPostCommentCardComponent } from './components/forum-post-comment-card/forum-post-comment-card.component';
import { AddEditForumPostCommentComponent } from './components/add-edit-forum-post-comment/add-edit-forum-post-comment.component';

@NgModule({
  declarations: [
    HomeComponent,
    EventsTabComponent,
    MaterialsTabComponent,
    ForumsTabComponent,
    EventCardComponent,
    AddEditEventComponent,
    ForumPostCardComponent,
    AddEditForumPostComponent,
    MaterialCardComponent,
    FileTypePipe,
    SubjectCategoryPipe,
    AddEditMaterialComponent,
    ForumPostCommentCardComponent,
    AddEditForumPostCommentComponent,
  ],
  imports: [
    CommonModule,
    MatTabsModule,
    MatCardModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    FormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatExpansionModule,
  ]
})
export class PlatformModule { }
