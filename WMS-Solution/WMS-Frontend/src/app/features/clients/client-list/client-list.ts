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
  ClientService
} from '../../../core/services/client.service';

import {
  Client
} from '../../../core/models/client';

@Component({
  selector: 'app-client-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './client-list.html',
  styleUrl: './client-list.scss'
})
export class ClientList
implements OnInit {

  clients: Client[] = [];

  activeClients = 0;

  inactiveClients = 0;

  constructor(

    private clientService:
    ClientService,

    private router: Router

  ) {}

  ngOnInit(): void {

    this.loadClients();

  }

  loadClients() {

    this.clientService
      .getClients()
      .subscribe({

        next: (data) => {

          this.clients = data;

          this.activeClients =
            data.filter(
              x => x.status
            ).length;

          this.inactiveClients =
            data.filter(
              x => !x.status
            ).length;

        },

        error: (err) => {

          console.log(err);

        }

      });

  }

  addClient() {

    this.router.navigate([
      '/clients/add'
    ]);

  }

  editClient(id:number) {

    this.router.navigate([
      '/clients/edit',
      id
    ]);

  }

  deleteClient(id:number) {

    if(confirm(
      'Delete Client?'
    )) {

      this.clientService
        .deleteClient(id)
        .subscribe({

          next: () => {

            alert(
              'Client Deleted Successfully'
            );

            this.loadClients();

          }

        });

    }

  }

}