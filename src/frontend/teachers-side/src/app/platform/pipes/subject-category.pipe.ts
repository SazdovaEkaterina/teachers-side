import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'subjectCategory',
})
export class SubjectCategoryPipe implements PipeTransform {
  transform(value: number): string {
    switch (value) {
      case 0:
        return 'Primary School';
      case 1:
        return 'High School';
      default:
        return 'Unknown Subject Category';
    }
  }
}
