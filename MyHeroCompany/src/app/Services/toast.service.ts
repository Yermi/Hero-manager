import { Injectable } from '@angular/core';
import { ToasterConfig, ToasterService } from 'angular2-toaster';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  constructor(private _toasterService: ToasterService) { }

  public _config: ToasterConfig =
  new ToasterConfig({
    showCloseButton: false,
    tapToDismiss: true,
    timeout: 4500,
    positionClass: 'toast-bottom-left'
  });

  public popToast(type: ToastType, msg: string) {
    this._toasterService.pop(type, msg);
  }
}

export enum ToastType {
  Success = "success",
  Error = "error"
}
