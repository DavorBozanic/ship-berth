import { Injectable } from '@angular/core';
import { environment } from '../common/configurations/environment';

@Injectable({
  providedIn: 'root'
})
export class ShipService {
  private readonly apiUrl: string = `${environment.apiUrl}/ship`;

  constructor() { }
}
