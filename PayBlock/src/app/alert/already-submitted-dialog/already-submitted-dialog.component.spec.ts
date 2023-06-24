import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlreadySubmittedDialogComponent } from './already-submitted-dialog.component';

describe('AlreadySubmittedDialogComponent', () => {
  let component: AlreadySubmittedDialogComponent;
  let fixture: ComponentFixture<AlreadySubmittedDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AlreadySubmittedDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AlreadySubmittedDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
