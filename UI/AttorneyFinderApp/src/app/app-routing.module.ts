import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AttorneyListComponent } from './components/attorney-list/attorney-list.component';
import { AttorneyDetailComponent } from './components/attorney-detail/attorney-detail.component';
import { AddAttorneyComponent } from './components/add-attorney/add-attorney.component';
import { EditAttorneyComponent } from './components/edit-attorney/edit-attorney.component';

const routes: Routes = [
  { path: 'attorneys', component: AttorneyListComponent },
  { path: 'add-attorney', component: AddAttorneyComponent },
  { path: 'attorney/:id', component: AttorneyDetailComponent },
  { path: 'edit-attorney/:id', component: EditAttorneyComponent },
  { path: '', redirectTo: 'attorneys', pathMatch: 'full' } // Default route
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

