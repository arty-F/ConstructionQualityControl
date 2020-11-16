import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CityComponent } from './city/city.component';
import { ShowCityComponent } from './city/show-city/show-city.component';
import { AddEditCityComponent } from './city/add-edit-city/add-edit-city.component';
import { SharedService } from './shared.service';

import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    CityComponent,
    ShowCityComponent,
    AddEditCityComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    ShowCityComponent, 
    AddEditCityComponent
  ],
  providers: [SharedService],
  bootstrap: [AppComponent]
})
export class AppModule { }
