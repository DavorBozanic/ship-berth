import { Component } from '@angular/core';
import { copyrightInformation } from '../../common/configurations/copyright';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { BackButtonDirective } from '../../common/directives/back-button.directive';
import { RegisterRequestDTO } from '../../services/models/RegisterRequestDTO';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, BackButtonDirective],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  public isPasswordHidden: boolean = true;
  public copyrightInformation: string = copyrightInformation;
  public registerForm: FormGroup;
  public registerSuccess: string = '';
  public registrationError: string = '';

  public constructor(private formBuilder: FormBuilder, private authService: AuthService) {
    this.registerForm = this.formBuilder.group(
      {
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        username: [
          '',
          [
            Validators.required,
            Validators.minLength(4),
            // Validators.pattern(regex.username),
          ],
        ],
        password: [
          '',
          [
            Validators.required,
            // Validators.pattern(regex.password)
          ],
        ],
        email: ['',
           [
            Validators.required, 
            // Validators.pattern(regex.email)
          ]
        ],
      },
    );
  }

  public onRegister(): void {
      const registerRequest: RegisterRequestDTO = this.registerForm.value;

     this.authService.register(registerRequest).subscribe({
      next: (response) => {
        
        if (response.success) {
          this.registerSuccess = response.message;
          
        } else {
          this.registrationError = response.message;
        }
      },
      error: (error) => {     
        this.registrationError = error.error.message;
      }
    });
  }
}

