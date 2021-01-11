import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Ability, Hero } from 'src/app/Models/Hero';

@Component({
  selector: 'app-hero',
  templateUrl: './hero.component.html',
  styleUrls: ['./hero.component.css']
})
export class HeroComponent implements OnInit {

  @Input()
  item: Hero;

  @Output()
  public trainHero = new EventEmitter<Hero>();

  Ability : typeof
  Ability = Ability;
  constructor() { }

  ngOnInit() {
    console.log(this.item);

  }

  Train() {
    console.log("Train");
    console.log(this.item);
    //logEnum[Enum.A]
    console.log(Ability[this.item.Ability]);

    this.trainHero.emit(this.item);
  }
}
