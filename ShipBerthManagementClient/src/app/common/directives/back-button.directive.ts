import { Directive, HostListener, inject } from '@angular/core';
import { Location } from '@angular/common';

@Directive({
  selector: '[appBackButton]',
  standalone: true
})
export class BackButtonDirective {
  private location = inject(Location);

  @HostListener('click')
  public onClick(): void {
    this.location.back();
  }
}