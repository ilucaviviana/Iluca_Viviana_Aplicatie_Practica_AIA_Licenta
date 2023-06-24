/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CitirePageComponent } from './citire-page.component';

describe('CitirePageComponent', () => {
  let component: CitirePageComponent;
  let fixture: ComponentFixture<CitirePageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CitirePageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CitirePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
