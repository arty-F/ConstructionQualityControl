import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'
import { AuthenticationComponent } from 'src/app/components/authentication/authentication.component'
import { RegistrationComponent } from 'src/app/components/registration/registration.component'
import { StartedComponent } from 'src/app/components/started/started.component'
import { OrdersPreviewComponent } from 'src/app/components/orders-preview/orders-preview.component'
import { UserGuard } from 'src/app/helpers/user.guard'
import { userRole } from 'src/app/models/user-roles'
import { OrdersNewRootComponent } from './components/orders-preview/orders-new/orders-new-root.component'
import { PaymentComponent } from './components/payment/payment.component'
import { WorksPreviewComponent } from './components/works-preview/works-preview.component'
import { WorkFindComponent } from './components/works-preview/work-find/work-find.component'
import { WorkInfoComponent } from './components/works-preview/work-info/work-info.component'
import { ChooseWorkerComponent } from './components/orders-preview/choose-worker/choose-worker.component'
import { OrderProgressComponent } from './components/order-progress/order-progress.component'

const routes: Routes = [
  { path: 'Auth', component: AuthenticationComponent },
  { path: 'Registration', component: RegistrationComponent },
  { path: '', component: StartedComponent },
  { path: 'Orders', component: OrdersPreviewComponent, canActivate: [UserGuard], data: {roles: [userRole.Customer]} },
  { path: 'Orders/NewOrder', component: OrdersNewRootComponent, canActivate: [UserGuard], data: {roles: [userRole.Customer]} },
  { path: 'Payment', component: PaymentComponent, canActivate: [UserGuard], data: {roles: [userRole.Customer]}},
  { path: 'ChooseWorker', component: ChooseWorkerComponent, canActivate: [UserGuard], data: {roles: [userRole.Customer]}},
  { path: 'Works', component: WorksPreviewComponent, canActivate: [UserGuard], data: {roles: [userRole.Builder]}},
  { path: 'Works/Find', component: WorkFindComponent, canActivate: [UserGuard], data: {roles: [userRole.Builder]}},
  { path: 'Works/Info', component: WorkInfoComponent, canActivate: [UserGuard], data: {roles: [userRole.Builder]}},
  { path: 'OrderProgress', component: OrderProgressComponent, canActivate: [UserGuard], data: {roles: [userRole.Builder, userRole.Customer]} }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
