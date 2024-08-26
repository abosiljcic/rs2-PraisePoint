import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  private products = [
    { id: 1, image: "https://assets.porsche.com/rs/beograd/-/media/Project/DealerWebsites/SharedDealersWebsite/Configurator-Teaser/911/911.jpg?rev=-1",  
      name: 'Product 1', price: 100 },
    { id: 2, image: "https://www.bobswatches.com/rolex-blog/wp-content/uploads/2019/01/Rolex_Submariner_16610V_5D3_5211-2-Edit-2-1.jpg", 
      name: 'Product 2', price: 200 },
    { id: 3, image: "https://i4.cloudfable.net/styles/735x735/128.133/Black/work-hard-nice-inspirational-positive-quote-coffee-mug-20240206033655-otq4eohr-s4.jpg", 
      name: 'Product 3', price: 300 },
      { id: 4, image: "https://images.pexels.com/photos/416528/pexels-photo-416528.jpeg?cs=srgb&dl=pexels-pixabay-416528.jpg&fm=jpg", 
      name: 'Product 4', price: 9999 },
      
  ];
  constructor() { }

  getProducts() {
    return this.products;
  }
}
