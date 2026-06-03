import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Result } from '../models/result.model';

@Injectable({ providedIn: 'root' })
export class ResultService {
  private apiUrl = 'https://localhost:7171/api/results';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Result[]> { return this.http.get<Result[]>(this.apiUrl); }
  getById(id: number): Observable<Result> { return this.http.get<Result>(`${this.apiUrl}/${id}`); }
  create(r: Result): Observable<Result> { return this.http.post<Result>(this.apiUrl, r); }
  update(id: number, r: Result): Observable<void> { return this.http.put<void>(this.apiUrl, r); }
  delete(id: number): Observable<void> { return this.http.delete<void>(`${this.apiUrl}/${id}`); }
}