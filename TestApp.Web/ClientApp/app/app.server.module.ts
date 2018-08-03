import { NgModule, ErrorHandler } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { AppRoutingModule } from './app.routing.module';
import { AppComponent } from './components/app/app.component';

@NgModule({
    imports: [
        ServerModule,
        AppRoutingModule
    ],

    bootstrap: [AppComponent]
})
export class AppModule {
}
