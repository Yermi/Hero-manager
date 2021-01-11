import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './Guards/auth.guard';
import { HerosComponent } from './Pages/heros/heros.component';
import { LoginComponent } from './Pages/login/login.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'heros', component: HerosComponent,canActivate: [AuthGuard] },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  //{ path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
