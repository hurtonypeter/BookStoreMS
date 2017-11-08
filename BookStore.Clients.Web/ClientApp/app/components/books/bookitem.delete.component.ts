import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '../../services/httpclient.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'bookitem-delete',
    templateUrl: './bookitem.delete.component.html'
})
export class BookItemDeleteComponent implements OnInit {
    bookItemId: any;
    bookId: any;

    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private route: ActivatedRoute,
        private router: Router) { }
    
    ngOnInit(): void {
        this.bookItemId = this.route.snapshot.paramMap.get('itemId');
        this.bookId = this.route.snapshot.paramMap.get('bookId');
    }

    deleteBookItem(): void {
        this.http.delete(this.baseUrl + ':6500/api/bookItems/' + this.bookItemId)
            .subscribe(resp => {
                this.router.navigate(['/books', this.bookId]);
            }, err => console.log(err));
    }
}