import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Question } from '../models/question.model';

@Injectable({ providedIn: 'root' })
export class QuestionService {
  private apiUrl = 'https://localhost:7171/api/questions';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Question[]> { return this.http.get<Question[]>(this.apiUrl); }
  getById(id: number): Observable<Question> { return this.http.get<Question>(`${this.apiUrl}/${id}`); }
  create(q: Question): Observable<Question> { return this.http.post<Question>(this.apiUrl, q); }
  update(id: number, q: Question): Observable<void> { return this.http.put<void>(this.apiUrl, q); }
  delete(id: number): Observable<void> { return this.http.delete<void>(`${this.apiUrl}/${id}`); }
}