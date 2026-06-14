import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { LeaveService }
from '../../../core/services/leave.service';

import { Leave }
from '../../../core/models/leave';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-leave-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './leave-list.html',
  styleUrl: './leave-list.scss'
})
export class LeaveList
implements OnInit {

  
  leaves: Leave[] = [];
  searchText = '';

  allLeaves: any[] = [];

  leave: any[] = [];

  pendingCount = 0;
  approvedCount = 0;
  rejectedCount = 0;
  role = localStorage.getItem('role');

  constructor(
    private leaveService: LeaveService,
    private router: Router
  ) {}

  ngOnInit(): void {

    this.loadLeaves();
    

  }

  loadLeaves() {

    this.leaveService
      .getLeaves()
      .subscribe({

        next: (data) => {

          this.leaves = data;
          this.allLeaves = data;


          this.pendingCount =
            data.filter(
              x => x.status === 'Pending'
            ).length;

          this.approvedCount =
            data.filter(
              x => x.status === 'Approved'
            ).length;

          this.rejectedCount =
            data.filter(
              x => x.status === 'Rejected'
            ).length;

        },

        error: (err) => {

          console.log(err);

        }

      });

  }

  addLeave() {

    this.router.navigate([
      '/leaves/add'
    ]);

  }

  editLeave(id: number) {

    this.router.navigate([
      '/leaves/edit',
      id
    ]);

  }

  approveLeave(id: number) {

    this.leaveService
      .approveLeave(id, 1)
      .subscribe({

        next: () => {

          alert(
            'Leave Approved'
          );

          this.loadLeaves();

        },

        error: (err) => {

          console.log(err);

        }

      });

  }

  rejectLeave(id: number) {

    this.leaveService
      .rejectLeave(id, 1)
      .subscribe({

        next: () => {

          alert(
            'Leave Rejected'
          );

          this.loadLeaves();

        },

        error: (err) => {

          console.log(err);

        }

      });

  }

  deleteLeave(id: number) {

    if(confirm(
      'Delete this leave request?'
    )) {

      this.leaveService
        .deleteLeave(id)
        .subscribe({

          next: () => {

            alert(
              'Leave Deleted'
            );

            this.loadLeaves();

          },

          error: (err) => {

            console.log(err);

          }

        });

    }

  }

  filterLeaves() {

  this.leaves =
    this.allLeaves.filter(x =>

      x.leaveType
        ?.toLowerCase()
        .includes(
          this.searchText.toLowerCase()
        )

    );

}

}