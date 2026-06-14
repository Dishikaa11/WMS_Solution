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

import { ProjectService }
from '../../../core/services/project.service';

@Component({
  selector: 'app-project-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './project-form.html',
  styleUrl: './project-form.scss'
})
export class ProjectForm
implements OnInit {

  projectForm: FormGroup;

  projectId = 0;

  isEditMode = false;

  constructor(

    private fb: FormBuilder,

    private projectService:
    ProjectService,

    private router: Router,

    private route: ActivatedRoute

  ) {

    this.projectForm =
      this.fb.group({

        projectName: [
          '',
          Validators.required
        ],

        clientId: [''],

        startDate: [''],

        endDate: [''],

        status: [
          'Active',
          Validators.required
        ]

      });

  }

  ngOnInit(): void {

    const id =
      this.route.snapshot.paramMap.get('id');

    if(id){

      this.isEditMode = true;

      this.projectId = +id;

      this.projectService
        .getProjectById(
          this.projectId
        )
        .subscribe({

          next: (data) => {

            this.projectForm
              .patchValue(data);

          }

        });

    }

  }

  goBack() {

    this.router.navigate([
      '/projects'
    ]);

  }

  onSubmit() {

    if(!this.projectForm.valid)
      return;

    if(this.isEditMode){

      this.projectService
        .updateProject(

          this.projectId,

          this.projectForm.value

        )
        .subscribe({

          next: () => {

            alert(
              'Project Updated Successfully'
            );

            this.router.navigate([
              '/projects'
            ]);

          }

        });

    }

    else {

      this.projectService
        .createProject(
          this.projectForm.value
        )
        .subscribe({

          next: () => {

            alert(
              'Project Added Successfully'
            );

            this.router.navigate([
              '/projects'
            ]);

          }

        });

    }

  }

}
