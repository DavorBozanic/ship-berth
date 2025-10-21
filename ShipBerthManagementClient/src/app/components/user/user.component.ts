import { Component } from '@angular/core';
import { DxButtonModule, DxDataGridModule, DxTextBoxModule } from 'devextreme-angular';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [DxDataGridModule, DxButtonModule, DxTextBoxModule],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent {
  users = [
      { id: 1, name: 'Alice', email: 'alice@example.com', role: 'Admin' },
      { id: 2, name: 'Bob', email: 'bob@example.com', role: 'User' },
      { id: 3, name: 'Charlie', email: 'charlie@example.com', role: 'User' },
    ];
}
