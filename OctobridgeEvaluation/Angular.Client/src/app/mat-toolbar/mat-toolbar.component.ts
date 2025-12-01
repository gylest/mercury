
import { Component, ChangeDetectionStrategy, signal, computed } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-mat-toolbar',
  standalone: true,
  templateUrl: './mat-toolbar.component.html',
  styleUrls: ['./mat-toolbar.component.css'],
  imports: [MatToolbarModule, MatButtonModule, MatIconModule, RouterLink],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MatToolbarComponent {

  navButtons = signal([
    { label: 'Home', active: true, route: '/' },
    { label: 'Attachments', active: false, route: '/manage-attachments' },
    { label: 'Customers', active: false, route: '/manage-customers' },
    { label: 'Orders', active: false, route: '/manage-orders' },
    { label: 'Products', active: false, route: '/manage-products' },
  ]);

  /**
   * Computed signal to safely determine the currently active label.
   */
  activeLabel = computed(() => {
    return this.navButtons().find(b => b.active)?.label ?? 'None';
  });

  // Method to handle button clicks (e.g., setting the visual active state)
  setActive(label: string): void {
    this.navButtons.update(buttons =>
      buttons.map(b => ({
        ...b,
        active: b.label === label
      }))
    );
    // console.log() removed as navigation is now handled by routerLink
  }

  // Method to get the currently active route
  get activeRoute(): string {
    return this.navButtons().find(b => b.active)?.route || '/';
  }

  // trackBy function for optimization
  trackByLabel(index: number, button: { label: string }): string {
    return button.label;
  }

  /**
 * Computed signal to safely determine the current route path.
 */
  currentRoutePath = computed(() => {
    return this.navButtons().find(b => b.active)?.route ?? '/';
  });
}
