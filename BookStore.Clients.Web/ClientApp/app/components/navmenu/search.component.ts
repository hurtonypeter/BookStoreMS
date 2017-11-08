import { Component, Inject, ViewChild, AfterViewInit } from '@angular/core';

import { HttpClient } from '../../services/httpclient.service';

import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/map';

@Component({
    selector: 'search',
    templateUrl: './search.component.html'
})
export class SearchComponent implements AfterViewInit {
    show: boolean = false;
    books = [];

    @ViewChild('input') input: any;

    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string) { }

    ngAfterViewInit(): any {
        this.input.valueChanges
            .debounceTime(1000)
            .subscribe((value: string) => {
                if (value) {
                    this.http.get(this.baseUrl + ':6503/api/search/' + value)
                        .subscribe(resp => {
                            this.books = resp.json();
                        });
                }
                else {
                    this.books = [];
                }
            });
    }

    termChanged(): void {
        
    }

    onBlur(): void {
        this.show = false;
    }

    onFocus(): void {
        this.show = true;
    }
}
