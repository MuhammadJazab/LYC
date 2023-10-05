import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoomsComponent } from './rooms/rooms.component';
import { ServicesComponent } from './services/services.component';
import { ProductsComponent } from './products/products.component';
import { PromotionComponent } from './promotion/promotion.component';
import { PackageComponent } from './package/package.component';
import { BranchesComponent } from './branches/branches.component';
import { StaffComponent } from './staff/staff.component';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { DepartmentComponent } from './departments/department.component';

const route: Routes =
  [
    {
      path: 'branches', component: BranchesComponent
    },
    {
      path: 'department', component: DepartmentComponent
    },
    {
      path: 'staff', component: StaffComponent
    },
    {
      path: 'rooms', component: RoomsComponent
    },
    {
      path: 'services', component: ServicesComponent
    },
    {
      path: 'products', component: ProductsComponent
    },
    {
      path: 'promotions', component: PromotionComponent
    },
    {
      path: 'packages', component: PackageComponent
    }
  ];


@NgModule({
  declarations: [
    BranchesComponent,
    StaffComponent,
    RoomsComponent,
    ServicesComponent,
    ProductsComponent,
    PromotionComponent,
    PackageComponent,
    DepartmentComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild(route)
  ],
  exports: [
    RouterModule
  ]
})
export class ConfigurationModule { }
