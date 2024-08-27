import { Component } from '@angular/core';
import { Router, RouterModule, RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { LoginComponent } from './components/login/login.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterModule,
    RouterOutlet,
	RouterLink,
	RouterLinkActive,
    LoginComponent
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'] 
})
export class AppComponent {
  title = 'AttorneyFinderApp';
}
