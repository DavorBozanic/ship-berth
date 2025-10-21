import { Component, OnInit } from '@angular/core';
import { copyrightInformation } from '../../common/constants/copyright';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { LoginRequestDTO } from '../../services/models/LoginRequestDTO';
import { trimFormValues } from '../../common/helpers/form-utility';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  public isPasswordHidden: boolean = true;
  public copyrightInformation: string = copyrightInformation;
  public loginForm: FormGroup;
  public authError: string = '';

  public constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
  ) {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  public onLogin(): void {
    trimFormValues(this.loginForm);

    const loginRequest: LoginRequestDTO = this.loginForm.value;

    this.authService.login(loginRequest).subscribe({
      next: (response) => {
        if (response) {
           this.router.navigate(['home']);
        }
      },
      error: (error) => {     
        this.authError = error.error.message;
      }
    });
  }

  public ngOnInit(): void {
    if (this.authService.isLoggedIn()) {
      this.router.navigate(['home']);
    }
  }
}
