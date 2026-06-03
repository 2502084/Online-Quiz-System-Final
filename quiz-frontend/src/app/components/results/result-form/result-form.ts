import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ResultService } from '../../../services/result';
import { Result } from '../../../models/result.model';

@Component({
  selector: 'app-result-form',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './result-form.html',
  styleUrl: './result-form.css'
})
export class ResultFormComponent implements OnInit {
  result: Result = { userId: 0, quizId: 0, score: 0 };
  isEdit = false;
  resultId!: number;

  constructor(
    private resultService: ResultService,
    private route: ActivatedRoute,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.resultId = +id;
      this.isEdit = true;
      this.resultService.getById(this.resultId).subscribe(data => {
        setTimeout(() => {
          this.result = {...data};
          this.cdr.detectChanges();
        }, 0);
      });
    }
  }

  save(): void {
    if (this.isEdit) {
      this.resultService.update(this.resultId, this.result).subscribe(() => {
        this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          this.router.navigate(['/results']);
        });
      });
    } else {
      this.resultService.create(this.result).subscribe(() => {
        this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          this.router.navigate(['/results']);
        });
      });
    }
  }
}