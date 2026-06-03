import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { QuestionService } from '../../../services/question';
import { Question } from '../../../models/question.model';

@Component({
  selector: 'app-question-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './question-list.html',
  styleUrl: './question-list.css'
})
export class QuestionListComponent implements OnInit {
  questions: Question[] = [];

  constructor(
    private questionService: QuestionService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void { this.load(); }

  load(): void {
    this.questionService.getAll().subscribe({
      next: (data) => {
        this.questions = [...data];
        this.cdr.detectChanges();
      },
      error: (err) => console.log("Error:", err)
    });
  }

  delete(id: number): void {
    if (confirm('Delete karna chahte ho?')) {
      this.questionService.delete(id).subscribe(() => this.load());
    }
  }
}