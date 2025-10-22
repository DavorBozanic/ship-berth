import { Component } from '@angular/core';
import { copyrightInformation } from '../../common/constants/copyright';

@Component({
  selector: 'app-reservation',
  standalone: true,
  imports: [],
  templateUrl: './reservation.component.html',
  styleUrl: './reservation.component.css'
})
export class ReservationComponent {
  public copyrightInformation: string = copyrightInformation;
}
