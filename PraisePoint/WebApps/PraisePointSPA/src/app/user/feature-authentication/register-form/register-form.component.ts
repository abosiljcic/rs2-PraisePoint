import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationFacadeService } from '../../domain/application-services/authentication-facade.service.ts.service';

interface IRegisterFormData {
  firstName: string;
  lastName: string;
  username: string;
  email: string;
  password: string;
  imageUrl: string;
  phoneNumber: string;
  companyId: string;
}

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.css'
})
export class RegisterFormComponent implements OnInit {

  public registrationForm: FormGroup;

  constructor(private authenticationService: AuthenticationFacadeService, private routerService: Router) {
    this.registrationForm = new FormGroup({
      firstName: new FormControl('', [Validators.required, Validators.minLength(3)]),
      lastName: new FormControl('', [Validators.required, Validators.minLength(3)]),
      username: new FormControl('', [Validators.required, Validators.minLength(5)]),
      email: new FormControl('', [Validators.required, Validators.minLength(12)]),
      password: new FormControl('', [Validators.required, Validators.minLength(8)]),
      phoneNumber: new FormControl('', [Validators.required, Validators.minLength(10)]),
      companyId: new FormControl('', [Validators.required, Validators.minLength(36)]),
      imageUrl: new FormControl('', [Validators.required, Validators.minLength(5)])
    });
  }

  ngOnInit(): void { }

  public onRegisterFormSubmit(): void {
    if (this.registrationForm.invalid) {
      window.alert('Form has errors!');
      return;
    }

    const data: IRegisterFormData = this.registrationForm.value as IRegisterFormData;

    if (this.registrationForm.valid) {
      this.authenticationService.register(data.firstName, data.lastName, data.username, data.password, data.imageUrl, data.email, 
        data.phoneNumber, data.companyId).subscribe((success: boolean) => {
          window.alert(`Registration ${success ? 'is' : 'is not'} successful!`);
          this.registrationForm.reset();
          if (success) {
            this.routerService.navigate(['/user', 'login']);
          }
        });
    }
  }

}
