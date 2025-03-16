// src/app/resolvers/tariff-data.resolver.ts
import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { TariffService } from '../../services/tariff.service';

@Injectable({
  providedIn: 'root'
})
export class ProductsJsonResolver implements Resolve<any> {
  constructor(private tariffService: TariffService) {}

  resolve() {
    return this.tariffService.getTariffData().pipe(
      catchError(error => {
        console.error('Resolver error:', error);
        return of({ error: 'Failed to load tariff data' });
      })
    );
  }
}