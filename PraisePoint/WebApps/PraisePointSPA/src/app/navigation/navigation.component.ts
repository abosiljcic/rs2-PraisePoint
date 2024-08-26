import { Component, OnInit } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faShop, faHome, faCartShopping } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-navigation',
  standalone: true,
  imports : [FontAwesomeModule],
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.css'
})
export class NavigationComponent implements OnInit {
  faShop = faShop;
  faCart = faCartShopping;
  faHome = faHome;

  constructor() { }

  ngOnInit(): void { }

}
