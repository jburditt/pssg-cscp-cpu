import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { NotificationBannerComponent } from './notification-banner/notification-banner.component';
import { IconStepperComponent } from './icon-stepper/icon-stepper.component';

@NgModule({
  declarations: [
    FooterComponent,
    HeaderComponent,
    IconStepperComponent,
    NotFoundComponent,
    NotificationBannerComponent,
  ],
  imports: [
    CommonModule
  ],
  exports: [
    FooterComponent,
    HeaderComponent,
    IconStepperComponent,
    NotFoundComponent,
    NotificationBannerComponent,
  ]
})
export class SharedModule { }
