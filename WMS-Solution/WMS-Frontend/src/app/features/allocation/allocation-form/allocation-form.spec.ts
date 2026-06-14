import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllocationForm } from './allocation-form';

describe('AllocationForm', () => {
  let component: AllocationForm;
  let fixture: ComponentFixture<AllocationForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AllocationForm],
    }).compileComponents();

    fixture = TestBed.createComponent(AllocationForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
