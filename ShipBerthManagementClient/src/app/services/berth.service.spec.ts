import { TestBed } from '@angular/core/testing';

import { BerthService } from './berth.service';

describe('BerthService', () => {
  let service: BerthService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BerthService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
