import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'LRS Project';
  constructor(private router: Router) {}
  isMap() {
    return this.router.url === '/map';
  }
}
