import { AgeGroup } from './age-group';

export class AgeGroupManager {
  static getAgeGroup(value: number) {
    var group = "";
    if (value <= 12) {
      group = AgeGroup.Child;
    }
    else if (value > 12 && value <= 18) {
      group = AgeGroup.Adolescence;
    }
    else if (value > 18 && value <= 59) {
      group = AgeGroup.Adult;
    }
    else if (value > 59) {
      group = AgeGroup.SeniorAdult;
    }
    return group;
  }
}


