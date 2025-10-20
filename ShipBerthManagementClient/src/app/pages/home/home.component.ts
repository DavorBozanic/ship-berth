import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { copyrightInformation } from '../../common/configurations/copyright';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  public copyrightInformation: string = copyrightInformation;
  
  public constructor(private authService: AuthService) {}

  public onLogout(): void {
    this.authService.logout();
  }
}
