import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '../../services/httpclient.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'book-edit',
    templateUrl: './book.edit.component.html'
})
export class BookEditComponent implements OnInit {
    book: any;

    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private route: ActivatedRoute,
        private router: Router) {

    }

    ngOnInit(): void {
        let id = this.route.snapshot.paramMap.get('bookId');
        if (id) {
            this.http.get(this.baseUrl + ':6500/api/books/' + id)
                .subscribe(resp => {
                    this.book = resp.json()
                }, error => console.log(error));
        }
        else {
            this.book = {};
        }
    }

    onSubmit(): void {
        let id = this.route.snapshot.paramMap.get('bookId');
        if (id) {
            this.http.put(this.baseUrl + ':6500/api/books/' + id, this.book)
                .subscribe(resp => {
                    this.router.navigate(['/books', id]);
                }, error => console.log(error));
        }
        else {
            this.http.post(this.baseUrl + ':6500/api/books', this.book)
                .subscribe(resp => {
                    this.router.navigate(['/books']);
                }, error => console.log(error));
        }
        
    }
}