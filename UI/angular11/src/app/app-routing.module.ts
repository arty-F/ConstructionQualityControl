import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'
import { AuthenticationComponent } from 'src/app/components/authentication/authentication.component'
import { CityComponent } from 'src/app/components/city/city.component'
import { RegistrationComponent } from 'src/app/components/registration/registration.component'
import { StartedComponent } from 'src/app/components/started/started.component'

const routes: Routes = [
  { path: 'Auth', component: AuthenticationComponent },
  { path: 'Registration', component: RegistrationComponent },
  { path: '', component: StartedComponent },
  { path: 'City', component: CityComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
