import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizFormComponent } from './quiz-form';

describe('QuizForm', () => {
  let component: QuizFormComponent;
  let fixture: ComponentFixture<QuizFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuizFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(QuizFormComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
