export class Hero {
  Id : number;
  Name : string;
  Ability : Ability;
  StartDate : Date;
  SuitColors : string;
  StartPower: number;
  CurrentPower: number;
  TrainerId: number;
}

export enum Ability {
  Attacker = 1,
  Defender = 2
}
