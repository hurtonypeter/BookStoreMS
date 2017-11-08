import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '../../services/httpclient.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'book-delete',
    templateUrl: './book.delete.component.html'
})
export class BookDeleteComponent implements OnInit {
    bookId: any;

    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private route: ActivatedRoute,
        private router: Router) { }

    ngOnInit(): void {
        this.bookId = this.route.snapshot.paramMap.get('bookId');
    }

    deleteBook(): void {
        this.http.delete(this.baseUrl + 'api/books/' + this.bookId)
            .subscribe(resp => {
                this.router.navigate(['/books']);
            }, err => console.log(err));
    }
}