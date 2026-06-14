import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserLoginList } from './user-login-list';

describe('UserLoginList', () => {
  let component: UserLoginList;
  let fixture: ComponentFixture<UserLoginList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserLoginList],
    }).compileComponents();

    fixture = TestBed.createComponent(UserLoginList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
