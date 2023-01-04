import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductDetailComponent } from './Components/product/product-detail/product-detail.component';
import { ProductComponent } from './components/product/product.component';

const routes: Routes = [
  {
    path: 'product',
    component: ProductComponent,
    pathMatch: 'full'
  },
  {
    path: 'product/:id',
    component: ProductDetailComponent,
    pathMatch: 'full'
  },
  {
    path: 'create-product',
    component: ProductDetailComponent,
    pathMatch: 'full'
  },


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
