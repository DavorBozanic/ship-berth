import { Component } from '@angular/core';
import { copyrightInformation } from '../../common/constants/copyright';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-not-found',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './not-found.component.html',
  styleUrl: './not-found.component.css'
})
export class NotFoundComponent {
   public copyrightInformation: string = copyrightInformation;
}
