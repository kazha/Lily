import { Pipe, PipeTransform } from '@angular/core';
import { AgeGroupManager } from '../common/age-group-manager';
import { AgeGroup } from '../common/age-group';

@Pipe({ name: 'ageGroupColor' })
export class AgeGroupColorPipe implements PipeTransform {

  transform(value: number): string {
    var group = AgeGroupManager.getAgeGroup(value);
    var result = "";
    switch (group) {
      case AgeGroup.Child:
        result = "age-child";
        break;
      case AgeGroup.Adolescence:
        result = "age-adolescence";
        break;
      case AgeGroup.Adult:
        result = "age-adult";
        break;
      case AgeGroup.SeniorAdult:
        result = "age-senior-adult";
        break;
    }
    return result;
  }

}
