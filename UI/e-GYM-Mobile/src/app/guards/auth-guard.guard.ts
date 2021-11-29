import { AuthService } from 'src/app/services/auth-service.ts/auth-service.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  async canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Promise<boolean> {
    let isLogged = await this.authService.IsLoggedIn()
    if (isLogged) {
      this.router.navigate(['home']);
      return false;
    }

    return true;
  }

}