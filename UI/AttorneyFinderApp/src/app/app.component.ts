import { Component } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { LoginComponent } from './components/login/login.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterModule,
    RouterOutlet,
    LoginComponent
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']  // Fixed typo: `styleUrl` to `styleUrls`
})
export class AppComponent {
  title = 'AttorneyFinderApp';
}
