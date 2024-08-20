import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttorneyDetailComponent } from './attorney-detail.component';

describe('AttorneyDetailComponent', () => {
  let component: AttorneyDetailComponent;
  let fixture: ComponentFixture<AttorneyDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AttorneyDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AttorneyDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
