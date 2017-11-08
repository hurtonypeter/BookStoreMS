import { Component, Inject } from '@angular/core';
import { HttpClient } from '../../services/httpclient.service';

@Component({
    selector: 'book-list',
    templateUrl: './book.list.component.html'
})
export class BookListComponent {
    books: any;

    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + ':6500/api/books').subscribe(res => {
            this.books = res.json();
        }, error => console.error(error));
    }
}