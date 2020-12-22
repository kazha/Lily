import { Pipe, PipeTransform } from '@angular/core';
import { AgeGroupManager } from '../common/age-group-manager';

@Pipe({ name: 'ageGroup' })
export class AgeGroupPipe implements PipeTransform {

  transform(value: number): string {
    return AgeGroupManager.getAgeGroup(value);
  }

}
