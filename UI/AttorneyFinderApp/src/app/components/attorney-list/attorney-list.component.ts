import { Component } from '@angular/core';
import { AttorneyService } from '../../services/attorney.service';
import { Attorney } from '../../models/attorney.model';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClient } from '@angular/common/http';



@Component({
  selector: 'app-attorney-list',
  standalone: true,
  imports: [CommonModule, RouterModule],  // Add RouterModule here
  templateUrl: './attorney-list.component.html',
  styleUrl: './attorney-list.component.css'
})


export class AttorneyListComponent {
  attorneys: Attorney[] = [];

  constructor(private attorneyService: AttorneyService, private http: HttpClient) {}

  ngOnInit(): void {
    this.loadAttorneys();
  }

  loadAttorneys(): void {
    this.attorneyService.getAttorneys().subscribe(data => {
      this.attorneys = data;
    });
  }

  deleteAttorney(id: number): void {
    if (confirm('Are you sure you want to delete this attorney?')) {
      this.attorneyService.deleteAttorney(id).subscribe(() => {
        this.loadAttorneys();
      });
    }
  }
}
