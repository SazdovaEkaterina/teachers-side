import {
  Component,
  EventEmitter,
  Inject,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { IPost } from '../../models/post';
import { ForumPostsService } from '../../service/forum-posts.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { catchError, EMPTY, Subject, takeUntil, tap } from 'rxjs';
import { UserService } from 'src/app/authentication/service/user.service';

@Component({
  selector: 'app-add-edit-forum-post',
  templateUrl: './add-edit-forum-post.component.html',
  styleUrls: ['./add-edit-forum-post.component.scss'],
})
export class AddEditForumPostComponent implements OnInit, OnDestroy {
  @Input() forumPost: IPost | null = null;
  @Input() forumId: number | undefined;
  @Output() closeAddEdit = new EventEmitter<boolean>();

  public title: string = '';
  public isEditMode: boolean = false;
  public formGroup: FormGroup;
  public errorMessage: string | undefined;

  private ngUnsubscribe = new Subject<void>();

  constructor(
    @Inject(FormBuilder) private readonly formBuilder: FormBuilder,
    @Inject(ForumPostsService)
    private readonly forumPostsService: ForumPostsService,
    @Inject(UserService) private readonly userService: UserService
  ) {
    this.formGroup = this.formBuilder.group({});
  }

  public ngOnInit(): void {
    this.initializeForm();
    this.title = this.forumPost ? 'Edit Forum Post' : 'Add Forum Post';
    this.isEditMode = this.forumPost ? true : false;
  }

  public ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  public submit(): void {
    const id = this.forumPost?.id ?? 0;
    const creator = this.userService.getUser;
    const payload = {
      ...this.formGroup.getRawValue(),
      creator,
      id,
      forum: { id: this.forumId },
    } as IPost;
    if (this.isEditMode) {
      this.forumPostsService
        .editForumPost(payload)
        .pipe(
          tap((result) => {
            this.closeAddEdit.emit(result);
            if (!result)
              this.errorMessage =
                'The edit was unsuccessful, please try again.';
          }),
          catchError((error) => {
            console.error(error);
            this.errorMessage = 'Something went wrong, please try again.';
            return EMPTY;
          }),
          takeUntil(this.ngUnsubscribe)
        )
        .subscribe();
    } else {
      this.forumPostsService
        .addForumPost(payload)
        .pipe(
          tap((result) => {
            this.closeAddEdit.emit(result);
            if (!result)
              this.errorMessage = 'The add was unsuccessful, please try again.';
          }),
          catchError((error) => {
            console.error(error);
            this.errorMessage = 'Something went wrong, please try again.';
            return EMPTY;
          }),
          takeUntil(this.ngUnsubscribe)
        )
        .subscribe();
    }
  }

  private initializeForm(): void {
    this.formGroup = this.formBuilder.group({
      title: [
        this.forumPost?.title ?? '',
        [Validators.required, Validators.maxLength(100)],
      ],
      content: [
        this.forumPost?.content ?? '',
        [Validators.required, Validators.maxLength(255)],
      ],
    });
  }
}
