import { Component, OnInit, Input } from '@angular/core';
import { IProduct } from '../../models/product'
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-product-item',
  //standalone: true,
  //imports: [],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.css'
})
export class ProductItemComponent implements OnInit{

  @Input() product!: IProduct;

  constructor(private cartService: CartService) { }

  addToCart(): void {
    // Pozivamo metodu iz CartService da dodamo proizvod u korpu
    this.cartService.addProduct({
      id: this.product.id,
      price: this.product.price,
      image: this.product.image,
      name: this.product.name
    });
  }

  ngOnInit(): void {
  }
}
