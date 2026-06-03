import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UserService } from '../../../services/user';
import { User } from '../../../models/user.model';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './user-list.html',
  styleUrl: './user-list.css'
})
export class UserListComponent implements OnInit {
  users: User[] = [];

  constructor(
    private userService: UserService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void { this.loadUsers(); }

  loadUsers(): void {
    this.userService.getAll().subscribe({
      next: (data) => {
        this.users = [...data];
        this.cdr.detectChanges();
      },
      error: (err) => console.log("Error:", err)
    });
  }

  delete(id: number): void {
    if (confirm('Delete karna chahte ho?')) {
      this.userService.delete(id).subscribe(() => this.loadUsers());
    }
  }
}