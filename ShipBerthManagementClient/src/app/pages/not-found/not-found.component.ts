import { Component } from '@angular/core';
import { copyrightInformation } from '../../common/constants/copyright';

@Component({
  selector: 'app-not-found',
  standalone: true,
  imports: [],
  templateUrl: './not-found.component.html',
  styleUrl: './not-found.component.css'
})
export class NotFoundComponent {
   public copyrightInformation: string = copyrightInformation;
}
