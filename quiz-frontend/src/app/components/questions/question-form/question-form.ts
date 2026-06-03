import { Component, OnInit ,ChangeDetectorRef} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { QuestionService } from '../../../services/question';
import { Question } from '../../../models/question.model';

@Component({
  selector: 'app-question-form',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './question-form.html',
  styleUrl: './question-form.css'
})
export class QuestionFormComponent implements OnInit {
  question: Question = {
    quizId: 0, questionText: '',
    optionA: '', optionB: '', optionC: '', optionD: '',
    correctAnswer: ''
  };
  isEdit = false;
  questionId!: number;

 constructor(
  private questionService: QuestionService,
  private route: ActivatedRoute,
  private router: Router,
  private cdr: ChangeDetectorRef
) {}
ngOnInit(): void {
  const id = this.route.snapshot.params['id'];
  if (id) {
    this.questionId = +id;
    this.isEdit = true;
    this.questionService.getById(this.questionId).subscribe(data => {
      setTimeout(() => {
        this.question = {...data};
        this.cdr.detectChanges();
      }, 0);
    });
  }
}

  save(): void {
    if (this.isEdit) {
      this.questionService.update(this.questionId, this.question).subscribe(() => {
        this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          this.router.navigate(['/questions']);
        });
      });
    } else {
      this.questionService.create(this.question).subscribe(() => {
        this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          this.router.navigate(['/questions']);
        });
      });
    }
  }}