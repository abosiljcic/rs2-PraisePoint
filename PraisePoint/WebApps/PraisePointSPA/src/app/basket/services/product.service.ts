import { Injectable } from '@angular/core';
import { IProduct } from '../models/product';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private products: Observable<IProduct[]> = new Observable<IProduct[]>();

  private readonly productsUrl = 'http://localhost:8006/api/v1/Products';

  constructor(private http: HttpClient) { }

  getProducts(): Observable<IProduct[]> {
    this.products = this.http.get<IProduct[]>(this.productsUrl);
    return this.products;
  }
}
