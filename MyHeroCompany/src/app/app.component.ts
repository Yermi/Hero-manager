import { Component } from '@angular/core';
import { ToastService } from './Services/toast.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(private toastService: ToastService) { }

  public _config = this.toastService._config;
}
