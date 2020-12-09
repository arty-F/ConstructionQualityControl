import { BrowserModule } from '@angular/platform-browser'
import { NgModule } from '@angular/core'
import { HttpClientModule } from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module'
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'

import { AppComponent } from './components/app.component'
import { CityComponent } from 'src/app/components/city/city.component'
import { ShowCityComponent } from 'src/app/components/city/show-city/show-city.component'
import { AddEditCityComponent } from 'src/app/components/city/add-edit-city/add-edit-city.component'
import { StartedComponent } from 'src/app/components/started/started.component'
import { AuthenticationComponent } from 'src/app/components/authentication/authentication.component'
import { RegistrationComponent } from 'src/app/components/registration/registration.component'
import { SharedService } from 'src/app/services/shared.service'

@NgModule({
  declarations: [
    AppComponent,
    CityComponent,
    ShowCityComponent,
    AddEditCityComponent,
    StartedComponent,
    AuthenticationComponent,
    RegistrationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule
  ],
  providers: [SharedService],
  bootstrap: [AppComponent]
})
export class AppModule { }
