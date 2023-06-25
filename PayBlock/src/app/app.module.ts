import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatSelectModule } from '@angular/material/select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InformationPageComponent } from './information-page/information-page.component';
import { DetailPageComponent } from './detail-page/detail-page.component';
import { ContactPageComponent } from './contact-page/contact-page.component';
import { SignPageComponent } from './sign-page/sign-page.component';
import { PayPageComponent } from './pay-page/pay-page.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { PlataPageComponent } from './plata-page/plata-page.component';
import { CitirePageComponent } from './citire-page/citire-page.component';
import { EuPageComponent } from './eu-page/eu-page.component';
import { SharedService } from './shared.service';

import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LogoutPageComponent } from './logout-page/logout-page.component';
import { AdminPageComponent } from './admin-page/admin-page.component';
import { ConfigComponent } from './config/config.component';
import { AuthDetailService } from './shared/auth-detail.service';
import { JwtInterceptor } from '@auth0/angular-jwt';
import { NgToastModule } from 'ng-angular-popup';
import { JwtModule } from '@auth0/angular-jwt';
import { EditUtilizatorComponent } from './edit-utilizator/edit-utilizator.component';
import { MatDialogModule } from '@angular/material/dialog';
import { AlertDialogComponent } from './alert/alert-dialog.component';
import { AlreadySubmittedDialogComponent } from './alert/already-submitted-dialog/already-submitted-dialog.component';
import { FooterComponent } from './footer/footer.component';
import { StripePaymentComponent } from './stripe-payment/stripe-payment.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTooltipModule } from '@angular/material/tooltip';

export function tokenGetter() {
  return localStorage.getItem('token');
}


@NgModule({
  declarations: [
    AppComponent,
    InformationPageComponent,
    DetailPageComponent,
    ContactPageComponent,
    SignPageComponent,
    PayPageComponent,
    NavBarComponent,
    AlertDialogComponent,
      RegisterPageComponent,
      PlataPageComponent,
      CitirePageComponent,
      EuPageComponent,
      LogoutPageComponent,
      AdminPageComponent,
      ConfigComponent,
      EditUtilizatorComponent,
      AlreadySubmittedDialogComponent,
      FooterComponent,
      StripePaymentComponent
   ],
  imports: [
    MatSelectModule,
    MatSnackBarModule,
     BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatTooltipModule,
    FormsModule,
    MatDialogModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    NgToastModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:44316"], // Add your domain here
        disallowedRoutes: [] // Add any routes here that you don't want to send the token with
      },
    })
  ],
  providers: [SharedService,
  AuthDetailService, JwtInterceptor],
  bootstrap: [AppComponent]
})
export class AppModule { }
