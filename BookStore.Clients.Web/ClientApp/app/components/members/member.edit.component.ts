import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '../../services/httpclient.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'member-edit',
    templateUrl: './member.edit.component.html'
})
export class MemberEditComponent implements OnInit {
    member: any;

    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private route: ActivatedRoute,
        private router: Router) {

    }

    ngOnInit(): void {
        let id = this.route.snapshot.paramMap.get('userId');
        if (id) {
            this.http.get(this.baseUrl + ':6502/api/members/' + id)
                .subscribe(resp => {
                    this.member = resp.json();
                }, error => console.log(error));
        }
        else {
            
        }
    }

    onSubmit(): void {
        let id = this.route.snapshot.paramMap.get('userId');
        if (id) {
            this.http.put(this.baseUrl + ':6502/api/members/' + id, this.member)
                .subscribe(resp => {
                    this.router.navigate(['/members', id]);
                }, error => console.log(error));
        }
        else {
            this.http.post(this.baseUrl + 'api/books', null)
                .subscribe(resp => {
                    
                }, error => console.log(error));
        }

    }
}