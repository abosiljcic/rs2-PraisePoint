import { Component, OnInit } from '@angular/core';
import { IProduct } from '../../models/product';
import { ProductService } from '../../services/product.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-product-list',
  //standalone: true,
  //imports: [],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent implements OnInit {

  products: IProduct[] = [];

  activeSubscriptions: Subscription[] = [];

  constructor(private productService: ProductService) {

    const sub = this.productService.getProducts()
      .subscribe((products) => {
        this.products = products;
        console.log('Products:', this.products);
      });
    this.activeSubscriptions.push(sub);
    
  }

  ngOnInit(): void {
  }

  ngOnDestroy() {
    this.activeSubscriptions.forEach((sub) => sub.unsubscribe);
  }
}
