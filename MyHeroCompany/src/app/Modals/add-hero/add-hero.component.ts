import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Ability, Hero } from 'src/app/Models/Hero';
import { ToastService, ToastType } from 'src/app/Services/toast.service';
import { TrainerService } from 'src/app/Services/trainer.service';

@Component({
  selector: 'app-add-hero',
  templateUrl: './add-hero.component.html',
  styleUrls: ['./add-hero.component.css']
})
export class AddHeroComponent implements OnInit {

  Name: string;
  Ability: number;
  StartPower: number;

  addHeroForm = this.fb.group({
    name: new FormControl('', [Validators.required]),
    ability: new FormControl('', [Validators.required]),
    power: new FormControl('', [Validators.required]),
  })

  constructor(
    public activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private trainerService: TrainerService,
    private toastService: ToastService
  ) { }

  ngOnInit() {
  }

  Save() {
    console.log(this.Name);
    console.log(this.Ability);
    console.log(this.StartPower);
    console.log(Number(localStorage.getItem("id")));
    var hero = <Hero>{
      Name: this.Name,
      Ability: Number(this.Ability),
      StartPower: this.StartPower,
      TrainerId: Number(localStorage.getItem("id"))
    };
    console.log(hero);
    this.trainerService.addHero(hero).subscribe(
      (res: Hero) => {
        if (res == null) {
          this.toastService.popToast(ToastType.Error, "Error inadding hero");
          return
        }
        console.log(res);
        this.toastService.popToast(ToastType.Success, "Hero successfully added");
        this.activeModal.close();
      },
      (err: HttpErrorResponse) => {
        console.log(err);
        this.toastService.popToast(ToastType.Error, err.error.Error);
      }
    )
  }
}
