import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAttorneyComponent } from './add-attorney.component';

describe('AddAttorneyComponent', () => {
  let component: AddAttorneyComponent;
  let fixture: ComponentFixture<AddAttorneyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddAttorneyComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddAttorneyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
