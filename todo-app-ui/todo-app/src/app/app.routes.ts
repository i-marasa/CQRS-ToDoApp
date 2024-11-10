import { Routes } from '@angular/router';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { AuthGuard } from './core/auth.guard';

export const routes: Routes = [
    {
      path: 'auth',
      component: AuthLayoutComponent,
      loadChildren: () => import('./features/auth/auth.module').then(m => m.AuthModule)
    },
    {
      path: 'todos',
      component: MainLayoutComponent,
      canActivate: [AuthGuard],  // Protect this route with AuthGuard
      loadChildren: () => import('./features/todos/todos.module').then(m => m.TodosModule)
    },
    {
      path: '',
      redirectTo: 'todos',
      pathMatch: 'full'
    },
    { path: '**', redirectTo: 'todos' } // Wildcard route for 404 page, can be customized
  ];
