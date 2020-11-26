import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthenticationComponent } from './authentication/authentication.component';
import {CityComponent} from './city/city.component';
import { StartedComponent } from './started/started.component';

const routes: Routes = [
  {path: 'Auth', component: AuthenticationComponent},
  {path: '', component: StartedComponent},
  {path: 'City', component: CityComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
