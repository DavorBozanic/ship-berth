import { Component, inject, OnInit } from '@angular/core';
import { DxButtonModule, DxDataGridModule, DxToastModule } from 'devextreme-angular';
import { ShipDTO } from '../models/ShipDTO';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { shipSizeRegex } from '../../common/constants/regex';
import { CommonModule } from '@angular/common';
import { trimFormValues } from '../../common/helpers/form-utility';
import { ShipService } from '../../services/ship.service';
import { catchError, of } from 'rxjs';
import { DxDataGridTypes } from 'devextreme-angular/ui/data-grid';

@Component({
  selector: 'app-ship',
  standalone: true,
  imports: [CommonModule, DxDataGridModule, ReactiveFormsModule, DxToastModule, DxButtonModule],
  templateUrl: './ship.component.html',
  styleUrl: './ship.component.css'
})
export class ShipComponent implements OnInit {
  public shipForm: FormGroup;
  public ships: ShipDTO[] = [];
  public shipError = '';
  public showToast = false;
  public toastDisplayTime = 1500;
  public activeTab: 'ships' | 'new' = 'ships';

  private formBuilder = inject(FormBuilder);
  private shipService = inject(ShipService);

  public constructor() {
    this.shipForm = this.formBuilder.group({
      name: ['', Validators.required],
      size: ['', [Validators.required, Validators.pattern(shipSizeRegex)]],
      type: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.loadShips();
  }

  private loadShips(): void {
    this.shipService.getShips()
      .pipe(
        catchError(err => {
          console.error('Error loading ships:', err);
          return of([]);
        })
      )
      .subscribe(ships => {
        this.ships = ships;
      });
  }

  public onEdit(event: DxDataGridTypes.ColumnButtonClickEvent): void{
    const editedShip = event.row?.data;
    console.log(editedShip);
  }

  public onDelete(event: DxDataGridTypes.ColumnButtonClickEvent): void{
    const deletedShip = event.row?.data.id;
    console.log(deletedShip);
  }

  public onSave(): void {
    trimFormValues(this.shipForm);
    
    const ship: ShipDTO = this.shipForm.value;

    this.shipService.createShip(ship).subscribe({
      next: () => {
        this.showToast = true;
        this.shipForm.reset();
        this.loadShips();
        this.activeTab = 'ships';
      },
      error: (error) => {     
        this.shipError = error.error.message;
      }
    });
  }
}
