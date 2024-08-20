import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ CommonModule, FormsModule ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}
  onLogin(): void {
    console.log('LoginComponent: Login attempt with username:', this.username);
    this.authService.login(this.username, this.password).subscribe({
      next: (response) => {
        console.log('LoginComponent: Login successful, navigating to attorneys.');
        this.authService.saveToken(response.token);
        console.log('LoginComponent: Token saved:', response.token);
        this.router.navigate(['/attorneys']);
      },
      error: (err) => {
        console.error('LoginComponent: Login failed:', err);
        this.errorMessage = 'Login failed. Please check your credentials and try again.';
      }
    });
  }
}

