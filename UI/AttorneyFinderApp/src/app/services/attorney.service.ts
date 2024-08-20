import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Attorney } from '../models/attorney.model';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AttorneyService {
  private apiUrl = 'http://localhost:8080/api/attorneys';

  constructor(private http: HttpClient, private authService: AuthService) {}

  getAttorneys(): Observable<Attorney[]> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.get<Attorney[]>(this.apiUrl, { headers });
  }

  getAttorney(id: number): Observable<Attorney> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.get<Attorney>(`${this.apiUrl}/${id}`, { headers });
  }

  addAttorney(attorney: Attorney): Observable<Attorney> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.post<Attorney>(this.apiUrl, attorney, { headers });
  }

  updateAttorney(attorney: Attorney): Observable<void> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.put<void>(`${this.apiUrl}/${attorney.id}`, attorney, { headers });
  }

  deleteAttorney(id: number): Observable<void> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.delete<void>(`${this.apiUrl}/${id}`, { headers });
  }
}
