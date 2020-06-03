import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ValidateComponent } from './components/validate/validate.component';
import { ConvertComponent } from './components/convert/convert.component';
import { SchemaComponent } from './components/schema/schema.component';

@NgModule({
  declarations: [
    AppComponent,
    ValidateComponent,
    ConvertComponent,
    SchemaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
