import { Injectable } from '@angular/core';
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

  private readonly tokenExpirationTime: string = 'expires_at';

  public constructor(private router: Router, private http: HttpClient) {}

  public login(loginData: LoginRequestDTO): Observable<LoginResponseDTO> {
    return this.http.post<LoginResponseDTO>(`${this.apiUrl}/login`, loginData)
      .pipe(
        tap(response => {
          if (response) {
            const payload: JWTPayload | null = this.validateToken(response.token);

            if (!payload) {
              throw new Error('Invalid token.');
            }

            this.setSession(payload);
          }
        }),
        catchError(err => {
          console.error('Login failed.', err.error?.message || err.message);
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

  private setSession(payload: JWTPayload): void {
    localStorage.setItem(
      this.tokenExpirationTime,
      moment(payload.exp, 'seconds').unix().toString()
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
    return this.http.post<RegisterResponseDTO>(`${this.apiUrl}/register`, registerData);
  }
}