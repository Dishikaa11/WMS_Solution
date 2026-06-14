import { Component } from '@angular/core';

import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule
} from '@angular/forms';

import { CommonModule } from '@angular/common';

import { Router } from '@angular/router';

import { AuthService }
from '../../../core/services/auth.service';

@Component({
  selector:'app-change-password',
  standalone:true,
  imports:[
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl:'./change-password.html',
  styleUrl:'./change-password.scss'
})
export class ChangePassword {

  passwordForm: FormGroup;

  constructor(

    private fb: FormBuilder,

    private authService: AuthService,

    private router: Router

  ){

    this.passwordForm =
    this.fb.group({

      currentPassword:[
        '',
        Validators.required
      ],

      newPassword:[
        '',
        Validators.required
      ]

    });

  }

  onSubmit(){

    if(
      this.passwordForm.invalid
    ) return;

    const data = {

      username:
      localStorage.getItem(
        'username'
      ),

      currentPassword:
      this.passwordForm.value
      .currentPassword,

      newPassword:
      this.passwordForm.value
      .newPassword

    };
    console.log(data);

    this.authService
    .changePassword(data)
    .subscribe({

      next:(response)=>{

      console.log('SUCCESS:', response);

      alert(
        'Password Changed Successfully'
      );

      this.router.navigate([
        '/employee-dashboard'
      ]);

    },

      error:(err)=>{

        console.log('STATUS:', err.status);

        console.log('ERROR:', err.error);

        console.log('FULL ERROR:', err);

        alert('Password Change Failed');

      }

    });

  }

}