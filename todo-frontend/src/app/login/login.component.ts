import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'login',
  imports: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  constructor(private router: Router) {}

  login() {
    this.router.navigate(['/home']);
  }
}
