import { Component, inject } from '@angular/core';
import { copyrightInformation } from '../../common/constants/copyright';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { BackButtonDirective } from '../../common/directives/back-button.directive';
import { RegisterRequestDTO } from '../../services/models/RegisterRequestDTO';
import { emailRegex, passwordRegex, usernameRegex } from '../../common/constants/regex';
import { trimFormValues } from '../../common/helpers/form-utility';
import { DxToastModule } from 'devextreme-angular';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, BackButtonDirective, DxToastModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  public isPasswordHidden = true;
  public copyrightInformation: string = copyrightInformation;
  public registerForm: FormGroup;
  public showToast = false;
  public toastMessage = '';
  public toastType: 'success' | 'error' = 'success';

  private formBuilder = inject(FormBuilder);
  private authService = inject(AuthService);

  public constructor() {
    this.registerForm = this.formBuilder.group(
      {
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        username: [
          '',
          [
            Validators.required,
            Validators.minLength(4),
            Validators.maxLength(10),
            Validators.pattern(usernameRegex),
          ],
        ],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(8),
            Validators.maxLength(20),
            Validators.pattern(passwordRegex)
          ],
        ],
        email: ['',
           [
            Validators.required, 
            Validators.pattern(emailRegex)
          ]
        ],
      },
    );
  }

  public onRegister(): void {
    trimFormValues( this.registerForm);

    const registerRequest: RegisterRequestDTO = this.registerForm.value;

    this.authService.register(registerRequest).subscribe({
      next: (response) => {
        if (response.success) {
          this.toastType = "success";
          this.toastMessage = "User registered successfully."
          this.showToast = true;
          this.registerForm.reset();
        }
      },
      error: (error) => {
        this.toastType = "error";
        this.toastMessage = error.error.message;
        this.showToast = true;
      }
    });
  }
}

