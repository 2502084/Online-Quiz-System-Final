import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResultListComponent } from './result-list';

describe('ResultList', () => {
  let component: ResultListComponent;
  let fixture: ComponentFixture<ResultListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ResultListComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ResultListComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
