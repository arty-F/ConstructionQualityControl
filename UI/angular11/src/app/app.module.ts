import { BrowserModule } from '@angular/platform-browser'
import { NgModule } from '@angular/core'
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module'
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'

import { ErrorInterceptor } from 'src/app/helpers/error.interceptor'
import { JwtInterceptor } from 'src/app/helpers/jwt.interceptor'
import { AppComponent } from './components/app.component'
import { CityComponent } from 'src/app/components/city/city.component'
import { ShowCityComponent } from 'src/app/components/city/show-city/show-city.component'
import { AddEditCityComponent } from 'src/app/components/city/add-edit-city/add-edit-city.component'
import { StartedComponent } from 'src/app/components/started/started.component'
import { AuthenticationComponent } from 'src/app/components/authentication/authentication.component'
import { RegistrationComponent } from 'src/app/components/registration/registration.component'
import { SharedService } from 'src/app/services/shared.service'
import { AuthenticationService } from './services/authentication.service';
import { OrdersPreviewComponent } from './components/orders-preview/orders-preview.component';
import { OrdersNewRootComponent } from './components/orders-preview/orders-new/orders-new-root.component';
import { OrdersNewChildComponent } from './components/orders-preview/orders-new/order-tree/order-new-child.component';
import { PaymentComponent } from './components/payment/payment.component';
import { WorksPreviewComponent } from './components/works-preview/works-preview.component';
import { WorkFindComponent } from './components/works-preview/work-find/work-find.component';

@NgModule({
  declarations: [
    AppComponent,
    CityComponent,
    ShowCityComponent,
    AddEditCityComponent,
    StartedComponent,
    AuthenticationComponent,
    RegistrationComponent,
    OrdersPreviewComponent,
    OrdersNewRootComponent,
    OrdersNewChildComponent,
    PaymentComponent,
    WorksPreviewComponent,
    WorkFindComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    SharedService,
    AuthenticationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
