import { Component, inject, OnInit } from '@angular/core';
import { DxButtonModule, DxDataGridModule, DxPopupModule, DxTemplateModule, DxToastModule } from 'devextreme-angular';
import { BerthDTO } from '../models/BerthDTO';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { trimFormValues } from '../../common/helpers/form-utility';
import { BerthService } from '../../services/berth.service';
import { catchError, of } from 'rxjs';
import { BerthStatus } from '../enums/berth-status';

@Component({
  selector: 'app-berth',
  standalone: true,
  imports: [CommonModule, DxDataGridModule, DxPopupModule, ReactiveFormsModule, DxToastModule, DxButtonModule, DxTemplateModule],
  templateUrl: './berth.component.html',
  styleUrl: './berth.component.css'
})
export class BerthComponent implements OnInit {
  public berthForm: FormGroup;
  public berths: BerthDTO[] = [];
  public showToast = false;
  public toastMessage = '';
  public toastType: 'success' | 'error' = 'success';
  public activeTab: 'berths' | 'new' = 'berths';
  public isDeletePopupVisible = false;
  public isEditMode = false;

  public berthStatuses = [
    { id: BerthStatus.Available, name: 'Available' },
    { id: BerthStatus.Occupied, name: 'Occupied' }
  ];

  public editingBerthId: number | null = null;
  private selectedBerthId: number | null = null;

  private formBuilder = inject(FormBuilder);
  private berthService = inject(BerthService);

  public constructor() {
    this.berthForm = this.formBuilder.group({
      name: ['', Validators.required],
      location: ['', Validators.required],
      maxShipSize: ['', [Validators.required, Validators.max(100)]],
    });
  }

  ngOnInit(): void {
    this.loadBerths();
  }

  public switchToBerths(): void {
    this.activeTab = 'berths';
    this.isEditMode = false;
    this.editingBerthId = null;
    this.berthForm.reset();
  }

  private loadBerths(): void {
    this.berthService.getBerths()
      .pipe(
        catchError(err => {
          console.error('Error loading berths:', err);
          return of([]);
        })
      )
      .subscribe(berths => {
        this.berths = berths;
      });
  }

  public onEdit(berth: BerthDTO): void {
    this.isEditMode = true;
    this.editingBerthId = berth.id ?? null;

    this.berthForm.patchValue({
      name: berth.name,
      location: berth.location,
      maxShipSize: berth.maxShipSize
    });

    this.activeTab = 'new';
  }
  
  public onDelete(data: BerthDTO): void {
    const berth = data;
    
    if (berth) {
        this.selectedBerthId = berth.id ?? null;
        this.isDeletePopupVisible = true;
    }
  }

  public confirmDelete(): void {
    if (this.selectedBerthId) {
      this.berthService.deleteBerth(this.selectedBerthId).subscribe({
        next: () => {
          this.isDeletePopupVisible = false;
          this.toastType = "success";
          this.toastMessage = 'Berth deleted successfully.'
          this.showToast = true;
          this.loadBerths();
        },
        error: (error) => {
          console.error('Error deleting berth:', error);
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

  public onSave(): void {
    trimFormValues(this.berthForm);
    
    const berth: BerthDTO = this.berthForm.value;

    if (this.isEditMode && this.editingBerthId) {
      this.berthService.updateBerth(this.editingBerthId, berth).subscribe({
        next: () => {
          this.toastType = 'success';
          this.toastMessage = 'Berth updated successfully.';
          this.showToast = true;
          this.berthForm.reset();
          this.loadBerths();
          this.activeTab = 'berths';
          this.isEditMode = false;
          this.editingBerthId = null;
        },
        error: (error) => {
          this.toastType = 'error';
          this.toastMessage = error.error.message;
          this.showToast = true;
        }
      });
    } else {
      this.berthService.createBerth(berth).subscribe({
        next: () => {
          this.toastType = 'success';
          this.toastMessage = 'Berth created successfully.';
          this.showToast = true;
          this.berthForm.reset();
          this.loadBerths();
          this.activeTab = 'berths';
        },
        error: (error) => {
          this.toastType = 'error';
          this.toastMessage = error.error.message;
          this.showToast = true;
        }
      });
    }
  }
}