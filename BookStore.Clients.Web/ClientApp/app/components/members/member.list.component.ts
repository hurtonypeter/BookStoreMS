import { Component, Inject } from '@angular/core';
import { HttpClient } from '../../services/httpclient.service';

@Component({
    selector: 'member-list',
    templateUrl: './member.list.component.html'
})
export class MemberListComponent {
    members: any = [];

    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + ':6502/api/members').subscribe(res => {
            this.members = res.json();
        }, error => console.error(error));
    }
}