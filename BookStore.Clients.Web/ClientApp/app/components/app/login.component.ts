import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '../../services/httpclient.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import { SecurityService } from '../../services/security.service';

import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'login',
    templateUrl: './login.component.html'
})
export class LoginComponent {
    email: string;
    password: string;

    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private route: ActivatedRoute,
        private router: Router,
        private securityService: SecurityService) { }
    
    onSubmit(): void {
        this.securityService.Authorize(this.email, this.password)
            .subscribe(res => {
                if (res) {
                    this.router.navigate(['/books']);
                }
            });
    }
}