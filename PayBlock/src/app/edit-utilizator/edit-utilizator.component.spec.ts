/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { EditUtilizatorComponent } from './edit-utilizator.component';

describe('EditUtilizatorComponent', () => {
  let component: EditUtilizatorComponent;
  let fixture: ComponentFixture<EditUtilizatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditUtilizatorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditUtilizatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
