import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '../../services/httpclient.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'member-details',
    templateUrl: './member.details.component.html'
})
export class MemberDetailsComponent implements OnInit {
    member: any;

    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private route: ActivatedRoute) {

    }

    ngOnInit(): void {
        this.route.paramMap
            .switchMap((params: ParamMap) => {
                return this.http.get(this.baseUrl + ':6502/api/members/' + params.get('userId'));
            })
            .subscribe(resp => { this.member = resp.json() }, error => console.log(error));

    }
}