import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { Router } from '@angular/router';

import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {

  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {

    this.loginForm = this.fb.group({

      username: [
        '',
        Validators.required
      ],

      password: [
        '',
        Validators.required
      ]

    });

  }

  onSubmit() {

    if (this.loginForm.invalid) {

      this.loginForm.markAllAsTouched();

      return;

    }

    this.authService
      .login(this.loginForm.value)
      .subscribe({

        next: (response) => {

  localStorage.setItem(
    'token',
    response.token
  );

  localStorage.setItem(
    'username',
    response.username
  );

  localStorage.setItem(
    'role',
    response.role
  );

  console.log(response);

  // Employee First Login
  if(

    response.role === 'Employee'

    &&

    response.isPasswordChanged === false

  ){

    this.router.navigate([
      '/change-password'
    ]);

    return;

  }

  // Admin
  if(
    response.role === 'Admin'
  ){

    this.router.navigate([
      '/admin-dashboard'
    ]);

    return;

  }

  // Manager
  if(
    response.role === 'Manager'
  ){

    this.router.navigate([
      '/manager-dashboard'
    ]);

    return;

  }

  // Employee
  this.router.navigate([
    '/employee-dashboard'
  ]);

},

        error: (error) => {

          console.error(
            'Login failed:',
            error
          );

          alert(
            'Invalid Username or Password'
          );

        }

      });

  }

}
