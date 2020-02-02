import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbDropdownModule, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NeonTextModule } from './common/components/neon-text/neon-text.module';
import { HomeComponent } from './common/containers/home/home.component';
import { ErrorDialogComponent } from './common/layouts/error-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    ErrorDialogComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModalModule,
    NgbDropdownModule,
    NeonTextModule
  ],
  providers: [],
  exports: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
