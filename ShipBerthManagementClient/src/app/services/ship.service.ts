import { inject, Injectable } from '@angular/core';
import { environment } from '../common/configurations/environment';
import { HttpClient } from '@angular/common/http';
import { ShipDTO } from '../components/models/ShipDTO';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShipService {
  private readonly apiUrl: string = `${environment.apiUrl}/ship`;

  private http = inject(HttpClient);

  public getUsers(): Observable<ShipDTO[]> {
    return this.http.get<ShipDTO[]>(`${this.apiUrl}`);
  }

  public createShip(): void {
    console.log();
  }
}
