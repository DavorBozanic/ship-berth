import { Component, OnInit } from '@angular/core';
import { DxDataGridModule } from 'devextreme-angular';
import { UserDTO } from '../models/UserDTO';
import { UserService } from '../../services/user.service';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [DxDataGridModule],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent implements OnInit {
  users: UserDTO[] = [];

  public constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  private loadUsers(): void {
    this.userService.getUsers()
      .pipe(
        catchError(err => {
          console.error('Error loading users:', err);
          return of([]);
        })
      )
      .subscribe(users => {
        this.users = users;
      });
  }
}
