import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Globals } from '../app.globals';
import { Ability, Hero } from '../Models/Hero';
import { Trainer } from '../Models/Trainer';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class TrainerService {

  trainer: Trainer;
  get Name() {
    return this.trainer ? this.trainer.Name : null;
  }

  get IsLoggedIn() {
    return localStorage.getItem('currentUser') != null;
  }

  private _heros: Hero[];

  public set heros(p_heros : Hero[]) {
    this._heros = p_heros;
  }

  public get heros() : Hero[] {
    return this._heros ? this._heros.sort((a, b) => { return a.CurrentPower - b.CurrentPower}) : null;
  }

  constructor(private http: HttpClient, private router: Router) { }

  trainersBaseUri = Globals.BASE_URL + "api/trainers";
  herosBaseUri = Globals.BASE_URL + "api/heros";

  Login(email: string, password: string) {
    var body = { Email: email, Password: password };
    return this.http.post(this.trainersBaseUri + "/login", body).pipe(
      tap(
        (res: Trainer) => {
          this.trainer = res;
          this.heros = res.Heros;
          var userData = window.btoa(email + ':' + password);
          console.log(userData);
          localStorage.setItem('currentUser', userData);
          localStorage.setItem('id', this.trainer.Id.toString());
        })
    );
  }

  logout() {
    this.trainer = null;
    localStorage.removeItem('currentUser');
    localStorage.removeItem('id');
    this.router.navigate(['/login']);
  }

  getAllHeros() {
    if (this.heros) {
      return of(this.heros)
    }
    else {
      var trainerId = localStorage.getItem('id');;
      return this.http.get(this.herosBaseUri + "?trainerId=" + trainerId).pipe(
        tap(
          (res: Hero[]) => {
            this.heros = res;
          })
      );
    }
  }

  addHero(hero: Hero) {
    return this.http.post(this.herosBaseUri, hero).pipe(
      tap(
        (res: Hero) => {
          if (res) {
            this.heros.push(res);
          }
        })
    );
  }

  TrainHero(hero: Hero) {
    var body = { trainerId: this.trainer.Id, heroId: hero.Id }
    return this.http.post(this.herosBaseUri + "/train", body)
  }
}
