import { Component, EventEmitter, Inject, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { IComment } from '../../models/comment';
import { ForumPostCommentsService } from '../../service/forum-post-comments.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { catchError, EMPTY, Subject, takeUntil, tap } from 'rxjs';
import { UserService } from 'src/app/authentication/service/user.service';

@Component({
  selector: 'app-add-edit-forum-post-comment',
  templateUrl: './add-edit-forum-post-comment.component.html',
  styleUrls: ['./add-edit-forum-post-comment.component.scss'],
})
export class AddEditForumPostCommentComponent implements OnInit, OnDestroy {
  @Input() comment: IComment | null = null;
  @Input() postId: number = 0;
  @Output() closeAddEdit = new EventEmitter<boolean>();

  public title: string = '';
  public isEditMode: boolean = false;
  public formGroup: FormGroup;
  public errorMessage: string | undefined;

  private ngUnsubscribe = new Subject<void>();

  constructor(
    @Inject(FormBuilder) private readonly formBuilder: FormBuilder,
    @Inject(ForumPostCommentsService)
    private readonly forumPostCommentsService: ForumPostCommentsService,
    @Inject(UserService) private readonly userService: UserService
  ) {
    this.formGroup = this.formBuilder.group({});
  }

  public ngOnInit(): void {
    this.initializeForm();
    this.title = this.comment ? 'Edit Comment' : 'Add Comment';
    this.isEditMode = this.comment ? true : false;
  }

  public ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  public submit(): void {
    const id = this.comment?.id ?? 0;
    const creator = this.userService.getUser;
    const payload = {
      ...this.formGroup.getRawValue(),
      creator,
      id,
      postId: this.postId,
    } as IComment;
    if (this.isEditMode) {
      this.forumPostCommentsService
        .editPostComment(payload)
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
      this.forumPostCommentsService
        .addPostComment(payload)
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
        this.comment?.title ?? '',
        [Validators.required, Validators.maxLength(100)],
      ],
      content: [
        this.comment?.content ?? '',
        [Validators.required, Validators.maxLength(255)],
      ],
    });
  }
}
