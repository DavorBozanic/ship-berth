import { inject, Injectable } from '@angular/core';
import { environment } from '../common/configurations/environment';
import { HttpClient } from '@angular/common/http';
import { BerthDTO } from '../components/models/BerthDTO';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BerthService {
  private readonly apiUrl: string = `${environment.apiUrl}/berths`;

  private http = inject(HttpClient);

  public getBerths(): Observable<BerthDTO[]> {
    return this.http.get<BerthDTO[]>(`${this.apiUrl}`).pipe(
      catchError((error) => {
        console.error('Error fetching berths:', error);
        
        return throwError(() => new Error('Failed to fetch berths.'));
      })
    );
  }

  public getBerth(id: number): Observable<BerthDTO> {
    return this.http.get<BerthDTO>(`${this.apiUrl}/${id}`).pipe(
      catchError((error) => {
        console.error('Error fetching berth:', error);

        return throwError(() => new Error('Failed to fetch berth.'));
      })
    );
  }

  public createBerth(berthDTO: BerthDTO): Observable<BerthDTO> {
    return this.http.post<BerthDTO>(this.apiUrl, berthDTO).pipe(
      catchError((error) => {
        console.error('Error creating berth:', error);

        return throwError(() => new Error('Failed to create berth.'));
      })
    );
  } 

  public deleteBerth(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      catchError((error) => {
        console.error('Error deleting berth:', error);

        return throwError(() => new Error('Failed to delete berth.'));
      })
    );
  }
}
