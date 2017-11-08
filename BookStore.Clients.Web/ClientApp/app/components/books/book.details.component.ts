import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '../../services/httpclient.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'book-details',
    templateUrl: './book.details.component.html'
})
export class BookDetailsComponent implements OnInit {
    book: any;

    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private route: ActivatedRoute) {
        
    }

    ngOnInit(): void {
        this.route.paramMap
            .switchMap((params: ParamMap) => {
                return this.http.get(this.baseUrl + ':6500/api/books/' + params.get('bookId'));
            })
            .subscribe(resp => { this.book = resp.json() }, error => console.log(error));
        
    }
}