import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UtilsService {

  constructor() { }

  getEntityNames(entities: any[]): string[] {
    let result: string[] = [];

    entities.forEach(entity => {
      result.push(entity.name);
    });

    return result;
  }

}
