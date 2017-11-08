import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';

import { SecurityService } from './security.service';

import { Observable } from 'rxjs/Observable';

@Injectable()
export class HttpClient {

    constructor(
        private http: Http,
        private securityService: SecurityService) { }

    appendAuthorizationHeader(headers: Headers) {
        headers.append('Authorization', 'Bearer ' + this.securityService.UserToken);
    }

    get(url: string): Observable<Response> {
        let headers = new Headers();
        this.appendAuthorizationHeader(headers);

        return this.http.get(url, {
            headers: headers
        });
    }

    post(url: string, data: any): Observable<Response> {
        let headers = new Headers();
        this.appendAuthorizationHeader(headers);

        return this.http.post(url, data, {
            headers: headers
        });
    }

    put(url: string, data: any): Observable<Response> {
        let headers = new Headers();
        this.appendAuthorizationHeader(headers);

        return this.http.put(url, data, {
            headers: headers
        });
    }

    delete(url: string): Observable<Response> {
        let headers = new Headers();
        this.appendAuthorizationHeader(headers);

        return this.http.delete(url, {
            headers: headers
        });
    }
}