import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { NgFor } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ResultService } from '../../../services/result';
import { Result } from '../../../models/result.model';

@Component({
  selector: 'app-result-list',
  standalone: true,
  imports: [NgFor, RouterModule],
  templateUrl: './result-list.html',
  styleUrl: './result-list.css',
})
export class ResultListComponent implements OnInit {
  results: Result[] = [];

  constructor(
    private resultService: ResultService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void { 
    this.load(); 
  }

  load(): void {
    this.resultService.getAll().subscribe({
      next: (data) => {
        this.results = [...data];
        this.cdr.detectChanges();
        console.log("Results:", this.results.length);
      },
      error: (err) => console.log("Error:", err)
    });
  }

  delete(id: number): void {
    if (confirm('Delete karna chahte ho?')) {
      this.resultService.delete(id).subscribe(() => this.load());
    }
  }
}