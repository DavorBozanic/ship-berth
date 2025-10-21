import { inject, Injectable } from '@angular/core';
import { environment } from '../common/configurations/environment';
import { HttpClient } from '@angular/common/http';
import { UserDTO } from '../components/models/UserDTO';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly apiUrl: string = `${environment.apiUrl}/user`;

  private http = inject(HttpClient);

  public getUsers(): Observable<UserDTO[]> {
    return this.http.get<UserDTO[]>(`${this.apiUrl}`);
  }
}
