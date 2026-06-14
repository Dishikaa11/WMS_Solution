import { Component, Inject, PLATFORM_ID, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';


@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss'
})
export class Navbar {

  
  username = '';

  role = '';
  

  constructor(
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: object
  ) {

    if (isPlatformBrowser(this.platformId)) {

      this.username =
        localStorage.getItem('username') ?? '';

    }

  }
  ngOnInit(): void {

  if (typeof window !== 'undefined') {

    this.username =
      localStorage.getItem('username') || '';

    this.role =
      localStorage.getItem('role') || '';

      

  }

}

  logout(): void {

    if (isPlatformBrowser(this.platformId)) {

      localStorage.clear();

    }

    this.router.navigate(['/login']);

  }

}
