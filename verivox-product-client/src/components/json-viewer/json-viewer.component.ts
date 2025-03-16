import { Component, OnInit } from '@angular/core';
import { TariffService } from '../../services/tariff.service';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-json-viewer',
  templateUrl: './json-viewer.component.html',
  styleUrls: ['./json-viewer.component.css'],
  standalone: true,
  imports: [CommonModule],
})
export class JsonViewerComponent {
  tariffData: any;
  errorMessage: string = '';

  constructor(private route: ActivatedRoute, private router: Router) {
    this.route.data.subscribe(({ tariffData, error }) => {
      if (error) {
        this.errorMessage = error;
        this.tariffData = null;
      } else {
        this.tariffData = tariffData;
        this.errorMessage = '';
      }
    });
  }

  navigateToComparison() {
    this.router.navigateByUrl('/');
  }
}