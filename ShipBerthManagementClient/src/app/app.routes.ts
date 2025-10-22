import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { HomeComponent } from './pages/home/home.component';
import { authGuard } from './services/guards/auth.guard';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { ShipComponent } from './components/ship/ship.component';
import { BerthComponent } from './components/berth/berth.component';
import { UserComponent } from './components/user/user.component';
import { ReservationComponent } from './components/reservation/reservation.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: 'home',
    component: HomeComponent,
    children: [
      { path: '', redirectTo: 'user', pathMatch: 'full' },
      {
        path: 'user',
        component: UserComponent,
      },
      {
        path: 'ship',
        component: ShipComponent,
      },
      {
        path: 'berth',
        component: BerthComponent,
      },
      {
        path: 'reservation',
        component: ReservationComponent,
      },
    ],
    canActivate: [authGuard]
  },
   { path: '**', component: NotFoundComponent },
];
