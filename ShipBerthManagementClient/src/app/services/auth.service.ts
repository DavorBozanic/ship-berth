import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import moment from 'moment';
import { Observable, of, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { RegisterRequestDTO } from './models/RegisterRequestDTO';
import { RegisterResponseDTO } from './models/RegisterResponseDTO';
import { environment } from '../common/configurations/environment';

interface JWTPayload {
  expires_in: string;
}

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private apiUrl = `${environment.apiUrl}/auth`;

  private readonly token: string =
    'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHBpcmVzX2luIjo3MjAwfQ.TmOFlmH23D89i_glot1u9b4EfGkgTon_itvsNc7LQOo';
  private readonly tokenExpirationTime: string = 'expires_at';

  public constructor(private router: Router, private http: HttpClient) {}

  public login(): Observable<boolean> {
    const result: boolean = Math.random() < 0.5;

    if (result) {
      const payload: JWTPayload | null = this.validateToken(this.token);

      if (!payload) {
        return of(false);
      }
      this.setSession(payload);
    }

    return of(result);
  }

  public logout(): void {
    this.removeSession();
    this.router.navigate(['login']);
  }

  private validateToken(token: string): JWTPayload | null {
    try {
      return jwtDecode<JWTPayload>(token);
    } catch {
      return null;
    }
  }

  private setSession(payload: JWTPayload): void {
    localStorage.setItem(
      this.tokenExpirationTime,
      moment().add(payload.expires_in, 'seconds').unix().toString()
    );
  }

  private removeSession(): void {
    localStorage.removeItem(this.tokenExpirationTime);
  }

  private getTokenExpiration(): moment.Moment | null {
    const expiresAt: string | null = localStorage.getItem(
      this.tokenExpirationTime
    );

    return expiresAt !== null ? moment.unix(parseInt(expiresAt)) : expiresAt;
  }

  private hasTokenExpired(): boolean {
    const tokenExpiration: moment.Moment | null = this.getTokenExpiration();

    return tokenExpiration ? moment().isAfter(tokenExpiration) : true;
  }

  public isLoggedIn(): boolean {
    return !this.hasTokenExpired();
  }

  public register(registerData: RegisterRequestDTO): Observable<RegisterResponseDTO> {
    return this.http.post<RegisterResponseDTO>(`${this.apiUrl}/register`, registerData)
      .pipe(
        tap(response => {
          if (response.success) {
            console.log('Registration successful:', response.message);
          }
        })
      );
  }
}