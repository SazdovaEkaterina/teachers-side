import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'fileType',
})
export class FileTypePipe implements PipeTransform {
  transform(value: number): string {
    switch (value) {
      case 0:
        return 'PDF';
      case 1:
        return 'DOCX';
      case 2:
        return 'XLS';
      case 3:
        return 'PPTX';
      default:
        return 'unknownÍ';
    }
  }
}
