import { Component } from '@angular/core';
import { LoaderService } from './Shared/Services/loader.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public loading$ = this.loader.loading$;
  constructor( private loader: LoaderService){}
}
