import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Globals } from 'src/app/app.globals';
import { CustomValidators } from 'src/app/Helpers/custom-validators';
import { Trainer } from 'src/app/Models/Trainer';
import { ToastService, ToastType } from 'src/app/Services/toast.service';


@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  Name: string;
  Email: string;
  Password: string;
  ConfirmPassword: string;
  StartTraining: Date;

  signupForm = this.fb.group({
    name: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.compose([
      Validators.required,
      CustomValidators.patternValidator(/\d/, { hasNumber: true }),
      CustomValidators.patternValidator(/[A-Z]/, { hasCapitalCase: true }),
      CustomValidators.patternValidator(/(?=.*?[#?!@$%^&*-])/, { hasSpecialCharacters: true }),
    ])),
    confirmPassword: new FormControl('', [Validators.required]),
    startDate: new FormControl('', [Validators.required]),
  },
  {
     validator: CustomValidators.passwordMatchValidator
  });

  constructor(
    public activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private http: HttpClient,
    private toastService: ToastService
    ) { }

  ngOnInit() {
  }

  SignUp() {
    console.log("SignUp");
    if (!this.signupForm.valid) {
      return;
    }
    var body = <Trainer>{ Name: this.Name, Email: this.Email, Password: this.Password, StartTraining: this.StartTraining };
    console.log(body);
    this.http.post(Globals.BASE_URL + "api/trainers/signUp", body).subscribe(
      (res) => {
        console.log(res);
        this.activeModal.close();
        this.toastService.popToast(ToastType.Success, "Successfully registered");
      },
      (err : HttpErrorResponse) => {
        console.log(err.error);
        this.toastService.popToast(ToastType.Error, err.error.Error);
      }
    );
  }
}
