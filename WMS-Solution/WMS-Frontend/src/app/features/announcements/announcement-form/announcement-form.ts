import { Component, OnInit } from '@angular/core';

import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule
}
from '@angular/forms';

import { CommonModule } from '@angular/common';

import {
  Router,
  ActivatedRoute
}
from '@angular/router';

import { AnnouncementService }
from '../../../core/services/announcement.service';

@Component({
  selector: 'app-announcement-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './announcement-form.html',
  styleUrl: './announcement-form.scss'
})
export class AnnouncementForm
implements OnInit {

  announcementForm: FormGroup;

  announcementId = 0;

  isEditMode = false;

  constructor(

    private fb: FormBuilder,

    private service:
    AnnouncementService,

    private router: Router,

    private route: ActivatedRoute

  ) {

    this.announcementForm =
      this.fb.group({

        title: [
          '',
          Validators.required
        ],

        message: [
          '',
          Validators.required
        ],

        createdBy: [1],

        createdOn: [
          new Date()
        ],

        isActive: [true]

      });

  }

  ngOnInit(): void {

    const id =
      this.route.snapshot.paramMap.get('id');

    if (id) {

      this.isEditMode = true;

      this.announcementId = +id;

      this.service
        .getAnnouncementById(
          this.announcementId
        )
        .subscribe({

          next: (data) => {

            this.announcementForm
              .patchValue(data);

          },

          error: (err) => {

            console.log(err);

          }

        });

    }

  }

  onSubmit() {

    if (!this.announcementForm.valid)
      return;

    if (this.isEditMode) {

      this.service
        .updateAnnouncement(

          this.announcementId,

          this.announcementForm.value

        )
        .subscribe({

          next: () => {

            alert(
              'Announcement Updated Successfully'
            );

            this.router.navigate([
              '/announcements'
            ]);

          }

        });

    }

    else {

      this.service
        .createAnnouncement(
          this.announcementForm.value
        )
        .subscribe({

          next: () => {

            alert(
              'Announcement Created Successfully'
            );

            this.router.navigate([
              '/announcements'
            ]);

          }

        });

    }

  }

  goBack() {

    this.router.navigate([
      '/announcements'
    ]);

  }

}