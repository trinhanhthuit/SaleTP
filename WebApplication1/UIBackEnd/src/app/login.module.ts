import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { BackEndService } from 'src/app/service/backEndService';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UtilService } from 'src/app/shared/util.service';// change
import { LoginComponent } from 'src/app/login.component';

@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    BackEndService,
    HttpClient,
    UtilService  ],
  bootstrap: [LoginComponent]
})
export class LoginModule { }
