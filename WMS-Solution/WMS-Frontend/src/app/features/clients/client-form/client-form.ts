import { Component, OnInit } from '@angular/core';

import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule
}
from '@angular/forms';

import { CommonModule }
from '@angular/common';

import {
  Router,
  ActivatedRoute
}
from '@angular/router';

import { ClientService }
from '../../../core/services/client.service';

@Component({
  selector: 'app-client-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './client-form.html',
  styleUrl: './client-form.scss'
})
export class ClientForm
implements OnInit {

  clientForm: FormGroup;

  clientId = 0;

  isEditMode = false;

  constructor(

    private fb: FormBuilder,

    private clientService:
    ClientService,

    private router: Router,

    private route: ActivatedRoute

  ) {

    this.clientForm =
      this.fb.group({

        clientName: [
          '',
          Validators.required
        ],

        clientAddress: [''],

        clientPhoneNumber: [''],

        clientLocation: [''],

        status: [true]

      });

  }

  ngOnInit(): void {

    const id =
      this.route.snapshot.paramMap.get('id');

    if(id){

      this.isEditMode = true;

      this.clientId = +id;

      this.clientService
        .getClientById(
          this.clientId
        )
        .subscribe({

          next: (data) => {

            this.clientForm
              .patchValue(data);

          }

        });

    }

  }

  goBack() {

    this.router.navigate([
      '/clients'
    ]);

  }

  onSubmit() {

    if(!this.clientForm.valid)
      return;

    if(this.isEditMode){

      this.clientService
        .updateClient(
          this.clientId,
          this.clientForm.value
        )
        .subscribe({

          next: () => {

            alert(
              'Client Updated Successfully'
            );

            this.router.navigate([
              '/clients'
            ]);

          }

        });

    }

    else {

      this.clientService
        .createClient(
          this.clientForm.value
        )
        .subscribe({

          next: () => {

            alert(
              'Client Added Successfully'
            );

            this.router.navigate([
              '/clients'
            ]);

          }

        });

    }

  }

}
