import { Router, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AttorneyListComponent } from './components/attorney-list/attorney-list.component';

export const appRoutes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'attorneys', component: AttorneyListComponent },
  { path: '**', redirectTo: '/login' }
];

