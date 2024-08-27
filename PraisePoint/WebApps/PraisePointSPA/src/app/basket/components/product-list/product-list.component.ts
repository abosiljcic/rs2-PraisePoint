import { Component, OnInit } from '@angular/core';
import { IProduct } from '../../models/product';
import { BasketService } from '../../services/basket.service';

@Component({
  selector: 'app-product-list',
  //standalone: true,
  //imports: [],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent implements OnInit {

  products : IProduct[] = [];

  constructor(private basketService: BasketService) { // Injektujemo BasketService

    this.products = this.basketService.getProducts();
    //console.log('Products:', this.products);
  }

  ngOnInit(): void {
  }
}
