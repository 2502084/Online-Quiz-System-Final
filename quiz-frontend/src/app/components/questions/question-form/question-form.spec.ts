import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionFormComponent } from './question-form';

describe('QuestionForm', () => {
  let component: QuestionFormComponent;
  let fixture: ComponentFixture<QuestionFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuestionFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(QuestionFormComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
