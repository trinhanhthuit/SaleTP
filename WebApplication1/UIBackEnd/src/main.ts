import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
import { LoginModule } from './app/login.module';
if (environment.production) {
  enableProdMode();
}
if (window.location.href.indexOf("login") != -1) {
  platformBrowserDynamic().bootstrapModule(LoginModule).catch(err => console.error(err));;
}

else {
  platformBrowserDynamic().bootstrapModule(AppModule).catch(err => console.error(err));
}


