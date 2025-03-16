import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TariffService {
  private apiUrl = 'https://localhost:7254/api/Tariff';

  constructor(private http: HttpClient) { }

  compareTariffs(consumption: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/compare?consumption=${consumption}`);
  }

  getTariffData(): Observable<any> {
    return this.http.get(`${this.apiUrl}/products`);
  }
}