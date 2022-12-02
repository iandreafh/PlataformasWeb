import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { InicioComponent } from './inicio/inicio.component';
import { ContactComponent } from './contact/contact.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppComponent,
    InicioComponent,
    ContactComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      { path: '', component: InicioComponent, pathMatch: 'full' },
      { path: 'contacto', component: ContactComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
