import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditAttorneyComponent } from './edit-attorney.component';

describe('EditAttorneyComponent', () => {
  let component: EditAttorneyComponent;
  let fixture: ComponentFixture<EditAttorneyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditAttorneyComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditAttorneyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
