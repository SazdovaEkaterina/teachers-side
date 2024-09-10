import { Component, Input } from '@angular/core';
import { IMaterial } from '../../models/material';

@Component({
  selector: 'app-material-card',
  templateUrl: './material-card.component.html',
  styleUrls: ['./material-card.component.scss'],
})
export class MaterialCardComponent {
  @Input() material: IMaterial | undefined = {
    id: 0,
    subject: {
      id: 0,
    },
    creator: {
      firstName: '',
      lastName: '',
      email: '',
    },
    dateCreated: new Date(),
    fileTitle: '',
    filePath: '',
    fileType: 0,
  };
}
