import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import moment from 'moment';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { RegisterRequestDTO } from './models/RegisterRequestDTO';
import { RegisterResponseDTO } from './models/RegisterResponseDTO';
import { environment } from '../common/configurations/environment';
import { LoginRequestDTO } from './models/LoginRequestDTO';
import { LoginResponseDTO } from './models/LoginResponseDTO';
import { JWTPayload } from './models/JWTPayload';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private readonly apiUrl: string = `${environment.apiUrl}/auth`;
  private readonly TOKEN_KEY = 'token';
  private readonly EXPIRES_AT_KEY = 'expires_at';

  private http = inject(HttpClient);
  private router = inject(Router);

  public login(loginData: LoginRequestDTO): Observable<LoginResponseDTO> {
    return this.http.post<LoginResponseDTO>(`${this.apiUrl}/login`, loginData).pipe(
      tap(response => {
        if (response?.token) {
          const payload = this.validateToken(response.token);

          if (!payload) {
            throw new Error('Invalid token.');
          }

          this.setSession(response.token, payload.exp);
        }
      }),
      catchError(err => {
        console.error('Login failed:', err.error?.message || err.message);
        return throwError(() => err);
      })
    );
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

  private setSession(token: string, exp: number): void {
    localStorage.setItem(this.TOKEN_KEY, token);
    localStorage.setItem(this.EXPIRES_AT_KEY, exp.toString());
  }

  private removeSession(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.EXPIRES_AT_KEY);
  }

  private getTokenExpiration(): moment.Moment | null {
    const expiresAt = localStorage.getItem(this.EXPIRES_AT_KEY);

    return expiresAt ? moment.unix(parseInt(expiresAt, 10)) : null;
  }

  private hasTokenExpired(): boolean {
    const expiration = this.getTokenExpiration();

    return !expiration || moment().isAfter(expiration);
  }

  public isLoggedIn(): boolean {
    return !this.hasTokenExpired() && !!this.getToken();
  }

  public getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  public register(registerData: RegisterRequestDTO): Observable<RegisterResponseDTO> {
    return this.http.post<RegisterResponseDTO>(`${this.apiUrl}/register`, registerData);
  }
}