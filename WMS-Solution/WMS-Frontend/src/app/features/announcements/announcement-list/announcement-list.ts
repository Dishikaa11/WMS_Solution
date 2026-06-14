import {
  Component,
  OnInit
} from '@angular/core';

import {
  CommonModule
} from '@angular/common';

import {
  Router
} from '@angular/router';

import {
  AnnouncementService
} from '../../../core/services/announcement.service';

import {
  Announcement
} from '../../../core/models/announcement';

@Component({
  selector: 'app-announcement-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './announcement-list.html',
  styleUrl: './announcement-list.scss'
})
export class AnnouncementList
implements OnInit {

  announcements:
  Announcement[] = [];

  activeCount = 0;

  inactiveCount = 0;
  role = localStorage.getItem('role');

  constructor(

    private announcementService:
    AnnouncementService,

    private router: Router

  ) {}

  ngOnInit(): void {

    this.loadAnnouncements();

  }

  loadAnnouncements() {

    this.announcementService
      .getAnnouncements()
      .subscribe({

        next: (data) => {

          this.announcements =
          data;

          this.activeCount =
          data.filter(
            x => x.isActive
          ).length;

          this.inactiveCount =
          data.filter(
            x => !x.isActive
          ).length;

        }

      });

  }

  addAnnouncement() {

    this.router.navigate([
      '/announcements/add'
    ]);

  }

  editAnnouncement(id:number) {

    this.router.navigate([
      '/announcements/edit',
      id
    ]);

  }

  deleteAnnouncement(id:number) {

    if(confirm(
      'Delete Announcement?'
    )) {

      this.announcementService
        .deleteAnnouncement(id)
        .subscribe({

          next: () => {

            this.loadAnnouncements();

          }

        });

    }

  }

}