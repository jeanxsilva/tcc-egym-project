import { LoaderService } from './services/loader-service/loader-service.service';
import { URL_BASE_GRAPHQL } from './../configs/config';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouteReuseStrategy } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// Ionic
import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { IonicStorageModule } from '@ionic/storage-angular';

// Apollo
import { APOLLO_OPTIONS } from 'apollo-angular';
import { HttpLink } from 'apollo-angular/http';
import { InMemoryCache } from '@apollo/client/core';

// Modules
import { AppRoutingModule } from './app-routing.module';
import { ManagementModule } from './modules/management/management.module';
import { AuthModule } from './modules/auth/auth.module';
import { FivAppBarModule } from '@fivethree/core';

// Services
import { AuthService } from './services/auth-service.ts/auth-service.service';

// Components
import { AppComponent } from './app.component';
import { LOCALE_ID } from '@angular/core';

import localePt from '@angular/common/locales/pt';
import { registerLocaleData } from '@angular/common';
registerLocaleData(localePt);

@NgModule({
  declarations: [AppComponent],
  entryComponents: [],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    IonicModule.forRoot(),
    AppRoutingModule,
    ManagementModule,
    AuthModule,
    FormsModule,
    FivAppBarModule,
    IonicStorageModule.forRoot()
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt-BR' }, ,
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    {
      provide: APOLLO_OPTIONS,
      useFactory: (httpLink: HttpLink) => {
        return {
          cache: new InMemoryCache(),
          defaultOptions: {
            query: {
              fetchPolicy: 'no-cache',
              errorPolicy: 'all',
            },
          },
          link: httpLink.create({
            uri: URL_BASE_GRAPHQL,
          }),
        };
      },
      deps: [HttpLink],
    },
    AuthService,
    LoaderService
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
