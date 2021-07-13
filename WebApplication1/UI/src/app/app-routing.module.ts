import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductComponent } from './FrontEnd/product.component';
import { HomeComponent } from './FrontEnd/home.component';
import { AboutComponent } from './FrontEnd/about.component';
import { ContactComponent } from './FrontEnd/contact.component';
import { ProductDetailComponent } from './FrontEnd/product-detail.component';
import { ServiceComponent } from './FrontEnd/service.component';
import { ServiceDetailComponent } from './FrontEnd/service-detail.component';
import { CartComponent } from './FrontEnd/cart.component';
import { NewsComponent } from './FrontEnd/news.component';
const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'trang-chu', redirectTo: '', pathMatch: 'full' },
  { path: 'san-pham/:id', component: ProductComponent },
  { path: 'detail/:id', component: ProductDetailComponent },
  { path: 'gioi-thieu', component: AboutComponent },
  { path: 'lien-he', component: ContactComponent },
  { path: 'dich-vu', component: ServiceComponent },
  { path: 'dich-vu/:id', component: ServiceDetailComponent },
  { path: 'cart', component: CartComponent },
  { path: 'tin-tuc', component: NewsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
