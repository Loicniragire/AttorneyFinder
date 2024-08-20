import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AttorneyService } from '../../services/attorney.service';
import { Attorney } from '../../models/attorney.model';
import { RouterModule } from '@angular/router'; 

@Component({
  selector: 'app-attorney-form',
  templateUrl: './attorney-form.component.html',
  standalone: true,
  imports: [
    RouterModule
  ],
  styleUrls: ['./attorney-form.component.css']
})

export class AttorneyFormComponent implements OnInit {
  attorneyForm!: FormGroup;
  isEditMode: boolean = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private attorneyService: AttorneyService
  ) { }

  ngOnInit(): void {
    this.attorneyForm = this.fb.group({
      name: [''],
      email: [''],
      phone: [''],
      // Add other necessary form controls
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.loadAttorneyData(id);
    }
  }

  loadAttorneyData(id: string): void {
    this.attorneyService.getAttorney(Number(id)).subscribe((data: Attorney) => {
      this.attorneyForm.patchValue(data);
    });
  }

  onSubmit(): void {
    if (this.isEditMode) {
      this.attorneyService.updateAttorney(this.attorneyForm.value).subscribe(() => {
        this.router.navigate(['/attorneys']);
      });
    } else {
      this.attorneyService.addAttorney(this.attorneyForm.value).subscribe(() => {
        this.router.navigate(['/attorneys']);
      });
    }
  }
}
