import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../../services/user';
import { User } from '../../../models/user.model';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './user-form.html',
  styleUrl: './user-form.css'
})
export class UserFormComponent implements OnInit {
  user: User = { name: '', email: '', password: '' };
  isEdit = false;
  userId!: number;

  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.userId = +id;
      this.isEdit = true;
      this.userService.getById(this.userId).subscribe(data => {
        setTimeout(() => {
          this.user = {...data};
          this.cdr.detectChanges();
        }, 0);
      });
    }
  }

  save(): void {
    if (this.isEdit) {
      this.userService.update(this.userId, this.user).subscribe(() => {
        this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          this.router.navigate(['/users']);
        });
      });
    } else {
      this.userService.create(this.user).subscribe(() => {
        this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          this.router.navigate(['/users']);
        });
      });
    }
  }
}