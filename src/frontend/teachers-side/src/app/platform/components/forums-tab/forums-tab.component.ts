import { Component, Inject, OnInit } from '@angular/core';
import { ISubject } from '../../models/subject';
import { SubjectsService } from '../../service/subjects.service';
import { IPost } from '../../models/post';
import { ForumPostsService } from '../../service/forum-posts.service';

@Component({
  selector: 'app-forums-tab',
  templateUrl: './forums-tab.component.html',
  styleUrls: ['./forums-tab.component.scss'],
})
export class ForumsTabComponent implements OnInit {
  public subjects: ISubject[] = [];
  public isSubjectsLoading: boolean = true;
  public selectedSubjectId: number | undefined = undefined;

  public forumPosts: IPost[] = [];
  public isForumPostsLoading: boolean = true;

  constructor(
    @Inject(SubjectsService) private readonly subjectsService: SubjectsService,
    @Inject(ForumPostsService)
    private readonly forumPostsService: ForumPostsService
  ) {}

  ngOnInit(): void {
    this.loadSubjects();
  }

  public onSubjectSelect(event: any) {
    this.selectedSubjectId = event.value;
    this.loadForumPosts();
  }

  private loadSubjects() {
    this.subjectsService.getSubjects().subscribe({
      next: (data: ISubject[]) => {
        this.subjects = data;
      },
      error: (error: any) => {
        console.error('Error fetching subjects', error);
      },
      complete: () => {
        this.isSubjectsLoading = false;
      },
    });
  }

  private loadForumPosts() {
    this.forumPostsService.getPosts(this.selectedSubjectId ?? 0).subscribe({
      next: (data: IPost[]) => {
        this.forumPosts = data;
      },
      error: (error: any) => {
        console.error('Error fetching forum posts', error);
      },
      complete: () => {
        this.isForumPostsLoading = false;
      },
    });
  }
}
