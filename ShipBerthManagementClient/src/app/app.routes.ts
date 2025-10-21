import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { HomeComponent } from './pages/home/home.component';
import { authGuard } from './services/guards/auth.guard';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { ShipComponent } from './components/ship/ship.component';
import { BerthComponent } from './components/berth/berth.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: 'home',
    component: HomeComponent,
    children: [
      { path: '', redirectTo: 'ship', pathMatch: 'full' },
      {
        path: 'ship',
        component: ShipComponent,
      },
      {
        path: 'berth',
        component: BerthComponent,
      },
    ],
    canActivate: [authGuard]
  },
   { path: '**', component: NotFoundComponent },
];
