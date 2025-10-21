import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BerthComponent } from './berth.component';

describe('BerthComponent', () => {
  let component: BerthComponent;
  let fixture: ComponentFixture<BerthComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BerthComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BerthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
