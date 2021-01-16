import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'
import { AuthenticationComponent } from 'src/app/components/authentication/authentication.component'
import { RegistrationComponent } from 'src/app/components/registration/registration.component'
import { StartedComponent } from 'src/app/components/started/started.component'
import { OrdersPreviewComponent } from 'src/app/components/orders-preview/orders-preview.component'
import { AuthGuard } from 'src/app/helpers/auth.guard'
import { UserGuard } from 'src/app/helpers/user.guard'
import { userRole } from 'src/app/models/user-roles'
import { OrdersNewRootComponent } from './components/orders-preview/orders-new/orders-new-root.component'
import { PaymentComponent } from './components/payment/payment.component'
import { WorksPreviewComponent } from './components/works-preview/works-preview.component'
import { WorkFindComponent } from './components/works-preview/work-find/work-find.component'
import { WorkInfoComponent } from './components/works-preview/work-info/work-info.component'

const routes: Routes = [
  { path: 'Auth', component: AuthenticationComponent },
  { path: 'Registration', component: RegistrationComponent },
  { path: '', component: StartedComponent },
  //{ path: 'City', component: CityComponent, canActivate: [AuthGuard] },
  { path: 'Orders', component: OrdersPreviewComponent, canActivate: [UserGuard], data: {roles: [userRole.Customer]} },
  { path: 'Orders/NewOrder', component: OrdersNewRootComponent },
  { path: 'Payment', component: PaymentComponent},
  { path: 'Works', component: WorksPreviewComponent},
  { path: 'Works/Find', component: WorkFindComponent},
  { path: 'Works/Info', component: WorkInfoComponent}
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
