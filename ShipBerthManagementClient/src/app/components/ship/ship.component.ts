import { Component, OnInit } from '@angular/core';
import { DxDataGridModule } from 'devextreme-angular';
import { ShipDTO } from '../models/ShipDTO';

@Component({
  selector: 'app-ship',
  standalone: true,
  imports: [DxDataGridModule],
  templateUrl: './ship.component.html',
  styleUrl: './ship.component.css'
})
export class ShipComponent implements OnInit {
  ships: ShipDTO[] = [];

  ngOnInit(): void {
    
  }
}
