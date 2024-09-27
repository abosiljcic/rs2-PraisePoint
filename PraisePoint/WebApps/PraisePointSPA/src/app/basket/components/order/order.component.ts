import { Component, OnInit } from '@angular/core';
import { ICart } from '../../models/cart';
import { CartService } from '../../services/cart.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IAppState } from '../../../shared/app-state/app-state';
import { Observable } from 'rxjs';
import { AppStateService } from '../../../shared/app-state/app-state.service';
import { Order } from '../../models/order';
import { ICartItem } from '../../models/cart-item';


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
      username: ['', Validators.required],
      email: ['', Validators.email],
      address: ['', Validators.required],
      country: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      zip: ['', Validators.required],
      sameAddress: [false],
      saveInfo: [false]
    });

    this.loadSavedData();
  }

  createOrder(): Order {
    const formValues = this.checkoutForm.value;
    const order: Order = {
      street: formValues.address,
      city: formValues.city,
      state: formValues.state,
      country: formValues.country,
      zipCode: formValues.zip,
      emailAddress: formValues.email,
      buyerId: "",
      buyerUsername: formValues.username,
      orderItems: this.getOrderItems() 
    };
    return order;
  }
  

  onSubmit(): void {
    if (this.checkoutForm.valid) {
      console.log('Form Submitted', this.checkoutForm.value);

      const order = this.createOrder();
      this.cartService.checkout(order).subscribe({
        next: (response) => {
          console.log('successful checkout', response);
        },
        error: (err) => {
          console.error('Error checkout:', err);
        }
      });

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
    return this.cart.products.map(item => ({
      productName: item.product.productName,
      productId: item.product.productId,
      pictureUrl: item.product.pictureUrl,
      price: item.product.price,
      quantity: item.quantity
    }));
  } 

  getOrderItems(): any[] {
    return this.getCartProducts().map(item => ({
      productName: item.productName,
      productId: item.productId,
      pictureUrl: item.pictureUrl,
      price: item.price,
      units: item.quantity
    }));
  }
}
