import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminPageComponent } from './admin-page/admin-page.component';
import { CitirePageComponent } from './citire-page/citire-page.component';
import { ContactPageComponent } from './contact-page/contact-page.component';
import { DetailPageComponent } from './detail-page/detail-page.component';
import { EuPageComponent } from './eu-page/eu-page.component';
import { InformationPageComponent } from './information-page/information-page.component';
import { PlataPageComponent } from './plata-page/plata-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { SignPageComponent } from './sign-page/sign-page.component';
import { AuthGuard } from './guard/auth.guard';
import { EditUtilizatorComponent } from './edit-utilizator/edit-utilizator.component';
import { StripePaymentComponent } from './stripe-payment/stripe-payment.component';


const routes: Routes = [

  {path: '', redirectTo: '/information', pathMatch:'full'},
  {path: 'information', component: InformationPageComponent, pathMatch:'full'},
  {path: 'information/:id', component: DetailPageComponent},
  {path: 'contact', component: ContactPageComponent},
  {path: 'sign', component: SignPageComponent},
  {path: 'stripe', component: StripePaymentComponent, canActivate:[AuthGuard]},
  {path: 'register', component: RegisterPageComponent},
  {path: 'detail', component: DetailPageComponent, canActivate:[AuthGuard]}, //user
  {path: 'plata', component: PlataPageComponent, canActivate:[AuthGuard]},
  {path: 'citire', component: CitirePageComponent, canActivate:[AuthGuard]},
  {path: 'eu', component: EuPageComponent, canActivate:[AuthGuard]},
  {path: 'edit/:id', component: EditUtilizatorComponent },
  {path: 'admin', component: AdminPageComponent, canActivate:[AuthGuard]} //admin

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
