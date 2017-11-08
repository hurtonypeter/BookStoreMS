import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '../../services/httpclient.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'rent',
    templateUrl: './rent.component.html'
})
export class RentComponent {
    rentMemberBarcode: string;
    rentBookBarcode: string;

    returnBookBarcode: string;

    rent: any = {};
    return: any = {};

    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string) {

    }

    rentHandler(): void {
        this.http.post(this.baseUrl + ':6500/api/rents/rent', this.rent)
            .subscribe(resp => {
                this.rent = {};
            }, err => alert('Error'));
    }

    returnHandler(): void {
        this.http.post(this.baseUrl + ':6500/api/rents/return', this.return)
            .subscribe(resp => {
                this.return = {};
            }, err => alert('Error'));
    }
}