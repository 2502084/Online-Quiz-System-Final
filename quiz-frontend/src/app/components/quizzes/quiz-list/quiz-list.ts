import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { QuizService } from '../../../services/quiz';
import { Quiz } from '../../../models/quiz.model';

@Component({
  selector: 'app-quiz-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './quiz-list.html',
  styleUrl: './quiz-list.css'
})
export class QuizListComponent implements OnInit {
  quizzes: Quiz[] = [];

  constructor(
    private quizService: QuizService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void { this.load(); }

  load(): void {
    this.quizService.getAll().subscribe({
      next: (data) => {
        this.quizzes = [...data];
        this.cdr.detectChanges();
      },
      error: (err) => console.log("Error:", err)
    });
  }

  delete(id: number): void {
    if (confirm('Delete karna chahte ho?')) {
      this.quizService.delete(id).subscribe(() => this.load());
    }
  }
}