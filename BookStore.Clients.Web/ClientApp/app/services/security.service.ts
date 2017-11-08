import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class SecurityService {

    private subj = new Subject<boolean>();

    public IsAuthorized: boolean;
    public UserToken: string;

    constructor(
        private http: Http,
        @Inject('BASE_URL') private baseUrl: string) { }

    public Authorize(email: string, password: string): Observable<boolean> {

        this.http.post(this.baseUrl + 'api/account/login', {
            email: email,
            password: password
        }).subscribe(resp => {
            this.IsAuthorized = true;
            let token = resp.text();
            this.UserToken = token.substring(1, token.length - 3);

            this.subj.next(true);
        }, err => {
            this.subj.next(false);
        });

        return this.subj.asObservable();
    }
}