import { Component } from '@angular/core';
import { RouterOutlet, RouterModule } from '@angular/router';
import { MatToolbarComponent } from './mat-toolbar/mat-toolbar.component';
import { FooterComponent } from './footer/footer.component';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  imports: [MatToolbarComponent, RouterOutlet,RouterModule, FooterComponent]
})
export class AppComponent {
  title = 'angularclient';
}
