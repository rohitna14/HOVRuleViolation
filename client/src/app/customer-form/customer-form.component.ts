import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface CheckRequest {
  make: string;
  model: string;
  weight: number;
  licensePlate: string;
}

interface CheckResponse {
  isViolation: boolean;
  message: string;
  customer?: {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    phone: string;
    licensePlate: string;
  };
  mastId: number;
  customerId: number;
  minWeight: number;
}

interface ErrorResponse {
  message: string;
  code?: string;
  minWeight?: number;
}

@Component({
  selector: 'app-customer-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.css']
})
export class CustomerFormComponent {
  form = {
    make: '',
    model: '',
    weight: 0,
    licensePlate: '',
    notificationMethod: 'email'
  };

  violationChecked = false;
  violation = false;
  resultMessage = '';
  finalMessage = '';
  customer: any = null;
  minWeight = 0;
  loading = false;
  showWeightHint = false;

  constructor(private http: HttpClient) {}

  submitForm() {
    this.resetResults();
    this.loading = true;

    // Basic validation
    if (this.form.weight <= 0) {
      this.handleError({ message: '❌ Weight must be positive' });
      return;
    }

    const payload: CheckRequest = {
      make: this.form.make.trim(),
      model: this.form.model.trim(),
      weight: this.form.weight,
      licensePlate: this.form.licensePlate.trim()
    };
    console.log(payload); 

    this.http.post<CheckResponse | ErrorResponse>(
      'http://localhost:5265/api/HOVCheck/validate', 
      payload
    ).subscribe({
      next: (res) => {
        if ('isViolation' in res) {
          // Success response
          this.handleSuccess(res);
        } else {
          // Error response that still returned 200
          this.handleError(res);
        }
      },
      error: (err) => {
        this.handleError(err.error || { message: '❌ Request failed' });
      }
    });
  }

  private handleSuccess(res: CheckResponse) {
    console.log("Success")
    this.violation = res.isViolation;
    this.violationChecked = true;
    this.resultMessage = res.message;
    this.customer = res.customer;
    this.minWeight = res.minWeight;
    this.finalMessage = '✅ Transaction processed successfully';
    this.loading = false;
    this.showWeightHint = false;
  }

  private handleError(error: ErrorResponse) {
    this.resultMessage = error.message;
    console.log("error")
    // Special handling for weight errors
    if (error.code === 'WEIGHT_BELOW_MINIMUM' && error.minWeight) {
      this.minWeight = error.minWeight;
      this.showWeightHint = true;
    }

    this.violationChecked = false;
    this.finalMessage = '';
    this.loading = false;
  }

  sendNotification() {
    if (!this.customer) return;

    this.loading = true;
    this.finalMessage = '';

    this.http.post('http://localhost:5265/api/NotifyCustomer', {
      licensePlate: this.form.licensePlate,
      method: this.form.notificationMethod
    }).subscribe({
      next: () => {
        this.finalMessage = `✅ Notification sent via ${this.form.notificationMethod.toUpperCase()}`;
        this.loading = false;
      },
      error: (err) => {
        this.handleError(err.error || { message: '❌ Failed to send notification' });
      }
    });
  }

  resetForm() {
    this.form = {
      make: '',
      model: '',
      weight: 0,
      licensePlate: '',
      notificationMethod: 'email'
    };
    this.resetResults();
  }

  private resetResults() {
    this.violationChecked = false;
    this.resultMessage = '';
    this.finalMessage = '';
    this.customer = null;
    this.showWeightHint = false;
  }
}