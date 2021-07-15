import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductComponent } from 'src/app/backend/product.component';
import { CategoryComponent } from 'src/app/backend/category.component';
import { TestimonialComponent } from 'src/app/backend/testimonial.component';
import { ServiceComponent } from 'src/app/backend/service.component';
import { AboutComponent } from 'src/app/backend/about.component';
import { ContactComponent } from 'src/app/backend/contact.component';
import { LoginComponent } from './backend/login.component';
const routes: Routes = [
  { path: 'product', component: ProductComponent },
  { path: 'category', component: CategoryComponent },
  { path: 'testimonial', component: TestimonialComponent },
  { path: 'service', component: ServiceComponent },
  { path: 'about', component: AboutComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'login', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
