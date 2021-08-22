import { BrowserModule } from '@angular/platform-browser';
import { Injector, NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './pages/home/home.component';
import { CommandService } from './services/command.service';
import { CommandWrapper } from 'src/impl/CommandWrapper';

// export let AppInjector: Injector;
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [CommandService],
  bootstrap: [AppComponent]
})
export class AppModule {
   constructor(injector: Injector){
      // AppInjector = injector;
      CommandWrapper._service = injector.get<CommandService>(CommandService);
   }
 }
