import { Component } from '@angular/core';
import { TariffService } from '../../services/tariff.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-comparison',
  templateUrl: './comparison.component.html',
  styleUrls: ['./comparison.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule],
})
export class ComparisonComponent {
  consumption!: number;
  results: any[] = [];
  errorMessage: string = '';

  constructor(private tariffService: TariffService, private router: Router) { }

  compare() {
    if (this.consumption && this.consumption > 0) {
      this.tariffService.compareTariffs(this.consumption).subscribe({
        next: (data) => {
          this.results = data;
          this.errorMessage = '';
        },
        error: (err) => {
          this.errorMessage = 'Error fetching comparison results';
          console.error(err);
        }
      });
    } else {
      this.errorMessage = 'Please enter a valid consumption value';
    }
  }

  navigateToJsonViewer() {
    this.router.navigateByUrl('/json-viewer');
  }
}
