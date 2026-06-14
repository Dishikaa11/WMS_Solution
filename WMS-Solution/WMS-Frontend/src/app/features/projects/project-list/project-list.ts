import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { ProjectService }
from '../../../core/services/project.service';

import { Project }
from '../../../core/models/project';

@Component({
  selector: 'app-project-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './project-list.html',
  styleUrl: './project-list.scss'
})
export class ProjectList
implements OnInit {

  projects: Project[] = [];

  activeProjects = 0;
  completedProjects = 0;
  holdProjects = 0;

  constructor(
    private projectService: ProjectService,
    private router: Router
  ) {}

  ngOnInit(): void {

    this.loadProjects();

  }

  loadProjects() {

    this.projectService
      .getProjects()
      .subscribe({

        next: (data) => {

          this.projects = data;

          this.activeProjects =
            data.filter(
              x => x.status === 'Active'
            ).length;

          this.completedProjects =
            data.filter(
              x => x.status === 'Completed'
            ).length;

          this.holdProjects =
            data.filter(
              x => x.status === 'On Hold'
            ).length;

        },

        error: (err) => {

          console.log(err);

        }

      });

  }

  addProject() {

    this.router.navigate([
      '/projects/add'
    ]);

  }

  editProject(id: number) {

    this.router.navigate([
      '/projects/edit',
      id
    ]);

  }

  deleteProject(id: number) {

    if(confirm(
      'Delete Project?'
    )) {

      this.projectService
        .deleteProject(id)
        .subscribe({

          next: () => {

            alert(
              'Project Deleted Successfully'
            );

            this.loadProjects();

          },

          error: (err) => {

            console.log(err);

          }

        });

    }

  }

}
