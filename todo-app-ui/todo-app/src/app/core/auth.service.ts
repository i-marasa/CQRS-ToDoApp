import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7213/api/auth'; // Replace with your API base URL

  constructor(private http: HttpClient) {}

  // Register a new user
  register(userData: { username: string, email: string, password: string, confirmPassword: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, userData, this.httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  // Login an existing user
  login(credentials: { username: string, password: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, credentials, this.httpOptions)
      .pipe(
        map((response: any) => {
          // Store token if login is successful
          if (response && response.token) {
            localStorage.setItem('authToken', response.token);
          }
          return response;
        }),
        catchError(this.handleError)
      );
  }

  // Logout user
  logout(): void {
    localStorage.removeItem('authToken');
  }
  
  isAuthenticated(): boolean {
    return !!localStorage.getItem('authToken');
  }

  // Get HTTP options with JSON headers
  private get httpOptions() {
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
  }

  // Handle errors
  private handleError(error: any): Observable<never> {
    console.error('An error occurred:', error);
    throw error;
  }
}
