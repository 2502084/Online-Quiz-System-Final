import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule, RouterOutlet],  // ← yeh dono zaroori hain
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class AppComponent {
  title = 'quiz-frontend';
}