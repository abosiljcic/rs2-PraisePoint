import { Component, OnInit } from '@angular/core';
import { ICart } from '../../models/cart';
import { CartService } from '../../services/cart.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IAppState } from '../../../shared/app-state/app-state';
import { Observable } from 'rxjs';
import { AppStateService } from '../../../shared/app-state/app-state.service';


@Component({
  selector: 'app-order',
  //standalone: true,
  //imports: [],
  templateUrl: './order.component.html',
  styleUrl: './order.component.css'
})
export class OrderComponent implements OnInit {

  cart: ICart;
  checkoutForm!: FormGroup;
  username: string | undefined;
  public appState$: Observable<IAppState>;

  constructor(private formBuilder: FormBuilder, private cartService: CartService, private appStateService: AppStateService) { 
    this.appState$ = this.appStateService.getAppState();
    this.cart =  { username: this.username, products: [], total: 0 };
  }

  ngOnInit(): void {
    this.initForm();
    this.cartService.cartDataObs$.subscribe((data: ICart) => {
      this.cart = data;
    });
    this.appState$.subscribe((state: IAppState) => {
      this.username = state.username
      console.log("Order Comp: Ovo je user:", this.username);
    });
  }

  private initForm(): void {
    this.checkoutForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.email],
      address: ['', Validators.required],
      address2: [''],
      country: ['', Validators.required],
      state: ['', Validators.required],
      zip: ['', Validators.required],
      sameAddress: [false],
      saveInfo: [false],
      paymentMethod: ['credit', Validators.required],
      ccName: ['', Validators.required],
      ccNumber: ['', Validators.required],
      ccExpiration: ['', Validators.required],
      ccCvv: ['', Validators.required]
    });

    this.loadSavedData();
  }

  onSubmit(): void {
    if (this.checkoutForm.valid) {
      console.log('Form Submitted', this.checkoutForm.value);
      if (this.checkoutForm.value.saveInfo) {
        localStorage.setItem('checkoutFormData', JSON.stringify(this.checkoutForm.value));
      } else {
        localStorage.removeItem('checkoutFormData');
        this.checkoutForm.reset();
      }
    } else {
      console.log('Form is invalid');
    }
  }

  loadSavedData(): void {
    // Load saved form data if available
    const savedData = localStorage.getItem('checkoutFormData');
    if (savedData) {
      this.checkoutForm.patchValue(JSON.parse(savedData));
    }
  }

  getCartProducts() {
    return this.cart.products;
  }
}
