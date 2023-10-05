import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppFooterComponent } from './Shared/Layout/app-footer/app-footer.component';
import { AppHeaderComponent } from './Shared/Layout/app-header/app-header.component';
import { AppLayoutComponent } from './Shared/Layout/app-layout/app-layout.component';
import { AppLeftmenuComponent } from './Shared/Layout/app-leftmenu/app-leftmenu.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { ConfigurationLayoutComponent } from './Shared/Layout/configuration-layout/configuration-layout.component';
import { ConfigurationLeftmenueComponent } from './Shared/Layout/configuration-leftmenue/configuration-leftmenue.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialogModule } from '@angular/material/dialog';
import { NetworkInterceptor } from './Shared/Interceptor/network.interceptor';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { DialogsComponent } from './Shared/Layout/dialogs/dialogs.component';
import { NgxImageCompressService } from 'ngx-image-compress';
import { CustomersLayoutComponent } from './Shared/Layout/customers-layout/customers-layout.component';

@NgModule({
  declarations: [
    AppComponent,
    AppLeftmenuComponent,
    AppHeaderComponent,
    AppFooterComponent,
    AppLayoutComponent,
    ConfigurationLayoutComponent,
    ConfigurationLeftmenueComponent,
    DialogsComponent,
    CustomersLayoutComponent
  ],
  imports: [
    MatSnackBarModule,
    MatProgressSpinnerModule,
    BrowserModule,
    HttpClientModule,
    RouterModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatDialogModule,

  ],
  exports: [

  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: NetworkInterceptor,
    multi: true,
  }, { provide: LocationStrategy, useClass: HashLocationStrategy }, NgxImageCompressService],
  bootstrap: [AppComponent]
})
export class AppModule { }
