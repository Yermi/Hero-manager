import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddHeroComponent } from 'src/app/Modals/add-hero/add-hero.component';
import { Ability, Hero } from 'src/app/Models/Hero';
import { ToastService, ToastType } from 'src/app/Services/toast.service';
import { TrainerService } from 'src/app/Services/trainer.service';

@Component({
  selector: 'app-heros',
  templateUrl: './heros.component.html',
  styleUrls: ['./heros.component.css']
})
export class HerosComponent implements OnInit {

  constructor(
    private trainerService: TrainerService,
    private toastService: ToastService,
    private modalService: NgbModal,
    ) { }

  heros: Hero[];

  ngOnInit() {
    this.trainerService.getAllHeros().subscribe(
      (res : Hero[]) => {
        this.heros = res;
      },
      (err : HttpErrorResponse) => {
        console.log(err);
        this.toastService.popToast(ToastType.Error, err.error.Error);
      }
    )
    this.heros = this.trainerService.heros;
  }

  logout() {
    this.trainerService.logout();
  }


  trainHero(hero: Hero) {
    console.log(hero);
    this.trainerService.TrainHero(hero).subscribe(
      (res : number) => {
        console.log(res);
        hero.CurrentPower = res;
        this.toastService.popToast(ToastType.Success, "Successfully trained hero");
      },
      (err : HttpErrorResponse) => {
        console.log(err);
        this.toastService.popToast(ToastType.Error, err.error.Error);
      }
    )
  }

  openHeroModal() {
    console.log("openHeroModal");
    this.modalService.open(AddHeroComponent);
  }
}
