import { Directive, HostListener } from '@angular/core';
import { Location } from '@angular/common';

@Directive({
  selector: '[appBackButton]',
  standalone: true
})
export class BackButtonDirective {
  public constructor(private location: Location) {}

  @HostListener('click')
  public onClick(): void {
    this.location.back();
  }
}