import { inject, Injectable } from '@angular/core';
import { environment } from '../common/configurations/environment';
import { HttpClient } from '@angular/common/http';
import { ShipDTO } from '../components/models/ShipDTO';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShipService {
  private readonly apiUrl: string = `${environment.apiUrl}/ships`;

  private http = inject(HttpClient);

  public getShips(): Observable<ShipDTO[]> {
    return this.http.get<ShipDTO[]>(`${this.apiUrl}`).pipe(
      catchError((error) => {
        console.error('Error fetching ships:', error);
        
        return throwError(() => new Error('Failed to fetch ships.'));
      })
    );
  }

  public getShip(id: number): Observable<ShipDTO> {
    return this.http.get<ShipDTO>(`${this.apiUrl}/${id}`).pipe(
      catchError((error) => {
        console.error('Error fetching ship:', error);

        return throwError(() => new Error('Failed to fetch ship.'));
      })
    );
  }

  public createShip(shipDto: ShipDTO): Observable<ShipDTO> {
    return this.http.post<ShipDTO>(this.apiUrl, shipDto).pipe(
      catchError((error) => {
        console.error('Error creating ship:', error);

        return throwError(() => new Error('Failed to create ship.'));
      })
    );
  } 

  public deleteShip(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      catchError((error) => {
        console.error('Error deleting ship:', error);

        return throwError(() => new Error('Failed to delete ship.'));
      })
    );
  }
}
