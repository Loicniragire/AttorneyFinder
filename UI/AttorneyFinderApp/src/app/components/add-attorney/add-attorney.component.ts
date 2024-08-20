import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Attorney } from '../../models/attorney.model';
import { FormsModule } from '@angular/forms';
import { AttorneyService } from '../../services/attorney.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-attorney',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-attorney.component.html',
  styleUrl: './add-attorney.component.css'
})
export class AddAttorneyComponent {
  newAttorney: Attorney = {
    id: 0, // You can set this to 0 or leave it out if your backend handles ID generation
    name: '',
    email: '',
    username: '',
    password: '',
    role: '',
    phone: '',
    isDeleted: false,
    createdAt: new Date(),
    updatedAt: new Date()
  };

  constructor(private attorneyService: AttorneyService, private router: Router) {}

  onSubmit(): void {
    this.attorneyService.addAttorney(this.newAttorney).subscribe(() => {
      // After successful submission, navigate back to the attorney list
      this.router.navigate(['/attorneys']);
    });
  }
}
