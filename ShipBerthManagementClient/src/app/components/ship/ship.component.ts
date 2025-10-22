import { Component, inject, OnInit } from '@angular/core';
import { DxButtonModule, DxDataGridModule, DxPopupModule, DxTemplateModule, DxToastModule } from 'devextreme-angular';
import { ShipDTO } from '../models/ShipDTO';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { shipSizeRegex } from '../../common/constants/regex';
import { CommonModule } from '@angular/common';
import { trimFormValues } from '../../common/helpers/form-utility';
import { ShipService } from '../../services/ship.service';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-ship',
  standalone: true,
  imports: [CommonModule, DxDataGridModule, DxPopupModule, ReactiveFormsModule, DxToastModule, DxButtonModule, DxTemplateModule],
  templateUrl: './ship.component.html',
  styleUrl: './ship.component.css'
})
export class ShipComponent implements OnInit {
  public shipForm: FormGroup;
  public ships: ShipDTO[] = [];
  public showToast = false;
  public toastMessage = '';
  public toastType: 'success' | 'error' = 'success';
  public activeTab: 'ships' | 'new' = 'ships';
  public isDeletePopupVisible = false;

  private selectedShipId: number | undefined;

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

  public onEdit(): void {
    console.log();
  }

  public onDelete(data: ShipDTO): void {
    const ship = data;
    if (ship) {
        this.selectedShipId = ship.id;
        this.isDeletePopupVisible = true;
    }
  }

  public confirmDelete(): void {
    if (this.selectedShipId) {
      this.shipService.deleteShip(this.selectedShipId).subscribe({
        next: () => {
          this.isDeletePopupVisible = false;
          this.toastType = "success";
          this.toastMessage = 'Ship deleted successfully.'
          this.showToast = true;
          this.loadShips();
        },
        error: (error) => {
          console.error('Error deleting ship:', error);
          this.isDeletePopupVisible = false;
          this.toastType = "error";
          this.toastMessage = error.error.message;
          this.showToast = true;
        }
      });
    }
  }

  public cancelDelete(): void {
    this.isDeletePopupVisible = false;
  }

  public onPopupHiding(): void {
    this.cancelDelete();
  }

  public onSave(): void {
    trimFormValues(this.shipForm);
    
    const ship: ShipDTO = this.shipForm.value;

    this.shipService.createShip(ship).subscribe({
      next: () => {
        this.toastType = "success";
        this.toastMessage = 'Ship created successfully.'
        this.showToast = true;
        this.shipForm.reset();
        this.loadShips();
        this.activeTab = 'ships';
      },
      error: (error) => {     
        this.toastType = "error";
        this.toastMessage = error.error.message;
        this.showToast = true;
      }
    });
  }
}
