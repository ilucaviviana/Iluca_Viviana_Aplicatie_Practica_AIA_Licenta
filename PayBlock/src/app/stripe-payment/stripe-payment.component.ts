import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
declare var Stripe: any;

@Component({
  selector: 'app-stripe-payment',
  templateUrl: './stripe-payment.component.html',
  styleUrls: ['./stripe-payment.component.css']
})
export class StripePaymentComponent implements OnInit {
  stripe: any;
  card: any;
  cardErrors: any;
  clientSecret: string;


  constructor(private http: HttpClient, private router: Router) {
    this.clientSecret = '';
  }


  @Input() invoices: any[] = [];
  invoice: any;

  ngOnInit() {

    this.invoice = history.state.invoice;
    this.stripe = new Stripe('pk_test_51NJ0V8KIQvqZ1VxJ2UrVZr78iiqnoeyFagCmMByEYdNo8yt6g4k7wGtoCigycv0BVcbvXPepxWci6QusZPkpSJEf00B5WQB691'); // Replace with your actual publishable key
    const elements = this.stripe.elements();

    this.card = elements.create('card');
    this.card.mount('#card-element');
    this.card.addEventListener('change', (event: any) => {
      this.cardErrors = event.error ? event.error.message : '';
    });

    //this.createPaymentIntent();
  }
  async handleForm(): Promise<void> {
    console.log('About to confirm card payment');
    console.log('Client Secret at confirmCardPayment: ', this.clientSecret);
    const { error, paymentIntent } = await this.stripe.confirmCardPayment(this.clientSecret, {
      payment_method: {
        card: this.card
      }
    });
    console.log('Card payment confirmation attempted');

    if (error) {
      console.log('Something is wrong:', error);
    } else if (paymentIntent.status === 'succeeded') {
      console.log('Success!', paymentIntent);
      alert('Successful payment! You will be redirected in 5 seconds.');
      setTimeout(() => this.router.navigate(['/plata']), 5000);
    }
  }



async pay(invoice: any): Promise<void> {
  console.log('Pay method started');
  let totalAmount = invoice.TotalApa + invoice.TotalRetim + invoice.TotalAdmin + invoice.TotalCuratenie;
  totalAmount = Math.round(totalAmount * 100);

  if (!this.clientSecret) {
    console.log('Client secret is not set, creating payment intent');
    await this.createPaymentIntent(totalAmount);
    console.log('Payment intent created');
  }

  console.log('About to handle form');
  await this.handleForm();
  console.log('Form handled');
}




testClick(): void {
  console.log('Button clicked');
}



async createPaymentIntent(totalAmount: number): Promise<void> {
  const items = [{id: 'invoice', amount: totalAmount}];
  try {
    const res = await this.http.post<any>('https://localhost:44316/api/Payment/create-payment-intent', {items}).toPromise();
    console.log('Response from server:', res);
    if (res && res.clientSecret) {
      this.clientSecret = res.clientSecret;
      console.log('Client Secret: ', this.clientSecret);
    } else {
      throw new Error('Client secret not found in server response');
    }
  } catch (error) {
    console.error('Error:', error);
    throw error;  // propagate the error up
  }
}



}


