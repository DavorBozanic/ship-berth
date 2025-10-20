import { Component } from '@angular/core';
import { copyrightInformation } from '../../common/configurations/copyright';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { BackButtonDirective } from '../../common/directives/back-button.directive';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, BackButtonDirective],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  public isPasswordHidden: boolean = true;
  public isPasswordConfirmHidden: boolean = true;
  public copyrightInformation: string = copyrightInformation;
  public registerForm: FormGroup;
  public registerSuccess: boolean = true;

  private passwordsEqualValidator(
    control: AbstractControl
  ): { passwordMatch: boolean } | null {
    const password = control.get('password')?.value;
    const passwordConfirm = control.get('passwordConfirm')?.value;

    return password !== passwordConfirm ? { passwordMatch: true } : null;
  }

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
        passwordConfirm: [
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
      { validators: this.passwordsEqualValidator }
    );
  }

  
  public onRegister(): void {
    this.authService.login().subscribe((authSuccess: boolean) => {
      this.registerSuccess = authSuccess;

      if (this.registerSuccess) {
        console.log('Test');
      }
    });
  }
}
