import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    // Check if the user is authenticated by calling the AuthService's method
    if (this.authService.isAuthenticated()) {
      return true; // Allow access if authenticated
    }

    // Redirect to the login page if not authenticated
    this.router.navigate(['/auth/login']);
    return false;
  }
}
