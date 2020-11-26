import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { CityComponent } from './city/city.component';
import { ShowCityComponent } from './city/show-city/show-city.component';
import { AddEditCityComponent } from './city/add-edit-city/add-edit-city.component';
import { SharedService } from './shared.service';
import { StartedComponent } from './started/started.component';
import { AuthenticationComponent } from './authentication/authentication.component';


@NgModule({
  declarations: [
    AppComponent,
    CityComponent,
    ShowCityComponent,
    AddEditCityComponent,
    StartedComponent,
    AuthenticationComponent
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
