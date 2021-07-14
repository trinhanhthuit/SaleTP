import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { AppComponent } from 'src/app/app.component';
import { BackEndService } from 'src/app/service/backEndService';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductComponent } from 'src/app/backend/product.component';
import { CategoryComponent } from 'src/app/backend/category.component';
import { TestimonialComponent } from 'src/app/backend/testimonial.component';
import { ServiceComponent } from 'src/app/backend/service.component';
import { AboutComponent } from 'src/app/backend/about.component';
import { ContactComponent } from 'src/app/backend/contact.component';
import { AddTextDirective } from './add-text.directive';
import { UtilService } from 'src/app/shared/util.service';// change

@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    CategoryComponent,
    TestimonialComponent,
    ServiceComponent,
    AboutComponent,
    ContactComponent,
    AddTextDirective
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
  bootstrap: [AppComponent]
})
export class AppModule { }
