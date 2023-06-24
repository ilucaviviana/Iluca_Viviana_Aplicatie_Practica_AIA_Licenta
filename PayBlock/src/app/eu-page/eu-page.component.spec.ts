/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { EuPageComponent } from './eu-page.component';

describe('EuPageComponent', () => {
  let component: EuPageComponent;
  let fixture: ComponentFixture<EuPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EuPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EuPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
