import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CommonModule } from "@angular/common";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FrontEndService } from 'src/app/Service/frontEndService';
import { HomeComponent } from './FrontEnd/home.component';
import { ProductComponent } from './FrontEnd/product.component';
import { ProductDetailComponent } from './FrontEnd/product-detail.component';
import { AboutComponent } from './FrontEnd/about.component';
import { ContactComponent } from './FrontEnd/contact.component';
import { ServiceComponent } from './FrontEnd/service.component';
import { ServiceDetailComponent } from './FrontEnd/service-detail.component';
import { CartComponent } from './FrontEnd/cart.component';
import { RouterModule } from '@angular/router';
import { NewsComponent } from './FrontEnd/news.component';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ProductComponent,
    ProductDetailComponent,
    AboutComponent,
    ContactComponent,
    ServiceComponent,
    ServiceDetailComponent,
    CartComponent,
    NewsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CommonModule,
    FormsModule,
    NgbModule,
    RouterModule
  ],
  providers: [
    FrontEndService,
    HttpClient],
  bootstrap: [AppComponent]
})
export class AppModule { }
