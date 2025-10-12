import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CustomerFormComponent } from './customer-form/customer-form.component';
import { HttpClientModule } from '@angular/common/http'; // ⬅️ ADD THIS LINE

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CustomerFormComponent, HttpClientModule], // ⬅️ ADD HERE TOO
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'client';
}
