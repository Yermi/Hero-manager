import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SignUpComponent } from '../../Modals/sign-up/sign-up.component';
import { Globals } from 'src/app/app.globals';
import { TrainerService } from 'src/app/Services/trainer.service';
import { Router } from '@angular/router';
import { Trainer } from 'src/app/Models/Trainer';
import { ToastService, ToastType } from 'src/app/Services/toast.service';
import { CustomErrorResponse } from 'src/app/Helpers/CustomErrorResponse';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  Email: string;
  Password: string;

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  });

  constructor(
    private modalService: NgbModal,
    private trainerService: TrainerService,
    private router: Router,
    private toastService: ToastService
  ) { }

  ngOnInit() {
  }

  Login() {
    console.log("login");
    if (!this.loginForm.valid) {
      return;
    }

    this.trainerService.Login(this.Email, this.Password).subscribe(
      (res : Trainer) => {
        console.log(res);
        this.toastService.popToast(ToastType.Success, "Successfully connected!");
        this.router.navigate(["/heros"])
      },
      (err : HttpErrorResponse) => {
        console.log(err);

        this.toastService.popToast(ToastType.Error, err.error.Error);
      }
    );
  }

  SignUp() {
    console.log("open signUp modal");
    this.modalService.open(SignUpComponent);
  }
}
