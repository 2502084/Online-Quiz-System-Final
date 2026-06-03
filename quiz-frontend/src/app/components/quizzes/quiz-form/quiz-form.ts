import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { QuizService } from '../../../services/quiz';
import { Quiz } from '../../../models/quiz.model';

@Component({
  selector: 'app-quiz-form',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './quiz-form.html',
  styleUrl: './quiz-form.css'
})
export class QuizFormComponent implements OnInit {
  quiz: Quiz = { title: '', description: '', timeLimit: 0 };
  isEdit = false;
  quizId!: number;

  constructor(
    private quizService: QuizService,
    private route: ActivatedRoute,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.quizId = +id;
      this.isEdit = true;
      this.quizService.getById(this.quizId).subscribe(data => {
        setTimeout(() => {
          this.quiz = {...data};
          this.cdr.detectChanges();
        }, 0);
      });
    }
  }

  save(): void {
    if (this.isEdit) {
      this.quizService.update(this.quizId, this.quiz).subscribe(() => {
        this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          this.router.navigate(['/quizzes']);
        });
      });
    } else {
      this.quizService.create(this.quiz).subscribe(() => {
        this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          this.router.navigate(['/quizzes']);
        });
      });
    }
  }
}