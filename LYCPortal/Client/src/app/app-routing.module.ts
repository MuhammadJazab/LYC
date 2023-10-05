import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppLayoutComponent } from './Shared/Layout/app-layout/app-layout.component';
import { ConfigurationLayoutComponent } from './Shared/Layout/configuration-layout/configuration-layout.component';
import { CustomersLayoutComponent } from './Shared/Layout/customers-layout/customers-layout.component';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('src/app/IdentityServer/identityserver.module').then(m => m.IdentityserverModule)
  },
  {
    path: '',
    component: AppLayoutComponent,
    loadChildren: () => import('src/app/DashBoard/dashboard.module').then(m => m.DashboardModule),
  },
  {
    path: 'configuration',
    component: ConfigurationLayoutComponent,
    loadChildren: () => import('src/app/Configuration/configuration.module').then(m => m.ConfigurationModule),
  },
  {
    path: '',
    component: CustomersLayoutComponent,
    loadChildren: () => import('src/app/Customers/customers.module').then(m => m.CustomersModule),
  }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
