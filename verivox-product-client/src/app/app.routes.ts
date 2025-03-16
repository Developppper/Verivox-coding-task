import { Routes } from '@angular/router';
import { ComparisonComponent } from '../components/comparison/comparison.component';
import { JsonViewerComponent } from '../components/json-viewer/json-viewer.component';
import { ProductsJsonResolver } from './resolvers/products-json.resolver';

export const routes: Routes = [
  { 
    path: 'json-viewer', 
    component: JsonViewerComponent,
    resolve: {
      tariffData: ProductsJsonResolver
    }
  },
  { path: '', component: ComparisonComponent },
  { path: '**', redirectTo: '' }
];
