import { FormGroup } from "@angular/forms";

export function trimFormValues(formGroup: FormGroup): void {
  if (!formGroup) return;

  Object.keys(formGroup.controls).forEach(controlName => {
    const control = formGroup.get(controlName);
    if (control && control.value && typeof control.value === 'string') {
      control.setValue(control.value.trim());
    }
  });
}
