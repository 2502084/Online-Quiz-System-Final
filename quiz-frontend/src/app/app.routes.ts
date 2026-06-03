import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'users', pathMatch: 'full' },

  { path: 'users', loadComponent: () => import('./components/users/user-list/user-list').then(m => m.UserListComponent) },
  { path: 'users/add', loadComponent: () => import('./components/users/user-form/user-form').then(m => m.UserFormComponent) },
  { path: 'users/edit/:id', loadComponent: () => import('./components/users/user-form/user-form').then(m => m.UserFormComponent) },

  { path: 'quizzes', loadComponent: () => import('./components/quizzes/quiz-list/quiz-list').then(m => m.QuizListComponent) },
  { path: 'quizzes/add', loadComponent: () => import('./components/quizzes/quiz-form/quiz-form').then(m => m.QuizFormComponent) },
  { path: 'quizzes/edit/:id', loadComponent: () => import('./components/quizzes/quiz-form/quiz-form').then(m => m.QuizFormComponent) },

  { path: 'questions', loadComponent: () => import('./components/questions/question-list/question-list').then(m => m.QuestionListComponent) },
  { path: 'questions/add', loadComponent: () => import('./components/questions/question-form/question-form').then(m => m.QuestionFormComponent) },
  { path: 'questions/edit/:id', loadComponent: () => import('./components/questions/question-form/question-form').then(m => m.QuestionFormComponent) },

  { path: 'results', loadComponent: () => import('./components/results/result-list/result-list').then(m => m.ResultListComponent) },
  { path: 'results/add', loadComponent: () => import('./components/results/result-form/result-form').then(m => m.ResultFormComponent) },
  { path: 'results/edit/:id', loadComponent: () => import('./components/results/result-form/result-form').then(m => m.ResultFormComponent) },
];