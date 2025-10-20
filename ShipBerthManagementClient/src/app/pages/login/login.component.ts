import { Component, OnInit } from '@angular/core';
import { copyrightInformation } from '../../common/configurations/copyright';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';

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
  public authSuccess: boolean = true;

  public constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  public onLogin(): void {
    this.authService.login().subscribe((authSuccess: boolean) => {
      this.authSuccess = authSuccess;

      if (authSuccess) {
        this.router.navigate(['home']);
      }
    });
  }

  public ngOnInit(): void {
    if (this.authService.isLoggedIn()) {
      this.router.navigate(['home']);
    }
  }
}
