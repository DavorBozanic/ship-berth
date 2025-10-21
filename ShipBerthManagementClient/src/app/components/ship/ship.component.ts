import { Component, OnInit } from '@angular/core';
import { DxDataGridModule } from 'devextreme-angular';
import { ShipDTO } from '../models/ShipDTO';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { shipSizeRegex } from '../../common/constants/regex';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-ship',
  standalone: true,
  imports: [CommonModule, DxDataGridModule, ReactiveFormsModule],
  templateUrl: './ship.component.html',
  styleUrl: './ship.component.css'
})
export class ShipComponent implements OnInit {
  public shipForm: FormGroup;
  public ships: ShipDTO[] = [];

  public constructor(private formBuilder: FormBuilder) {
    this.shipForm = this.formBuilder.group({
      name: ['', Validators.required],
      size: ['', [Validators.required, Validators.pattern(shipSizeRegex)]],
      type: ['', Validators.required],
    });
  }

  ngOnInit(): void {
  }

  public onSave(): void {
    console.log();
  }
}
