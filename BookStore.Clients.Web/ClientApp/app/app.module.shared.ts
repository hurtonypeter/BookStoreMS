import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { routing } from './app.routing';
import { AuthGuard } from './guards/auth';

import { HttpClient } from './services/httpclient.service';
import { SecurityService } from './services/security.service';
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { BookListComponent } from './components/books/book.list.component';
import { BookDetailsComponent } from './components/books/book.details.component';
import { BookDeleteComponent } from './components/books/book.delete.component';
import { BookEditComponent } from './components/books/book.edit.component';
import { BookItemEditComponent } from './components/books/bookitem.edit.component';
import { BookItemDeleteComponent } from './components/books/bookitem.delete.component';
import { LoginComponent } from './components/app/login.component';
import { MemberListComponent } from './components/members/member.list.component';
import { MemberDetailsComponent } from './components/members/member.details.component';
import { MemberEditComponent } from './components/members/member.edit.component';
import { RentComponent } from './components/rents/rent.component';
import { SearchComponent } from './components/navmenu/search.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        BookListComponent,
        BookDetailsComponent,
        BookDeleteComponent,
        BookEditComponent,
        BookItemEditComponent,
        BookItemDeleteComponent,
        LoginComponent,
        MemberListComponent,
        MemberDetailsComponent,
        MemberEditComponent,
        RentComponent,
        SearchComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', component: HomeComponent },

            { path: 'login', component: LoginComponent },

            { path: 'books', component: BookListComponent, canActivate: [AuthGuard] },
            { path: 'books/create', component: BookEditComponent, canActivate: [AuthGuard] },
            { path: 'books/:bookId', component: BookDetailsComponent, canActivate: [AuthGuard] },
            { path: 'books/:bookId/edit', component: BookEditComponent, canActivate: [AuthGuard] },
            { path: 'books/:bookId/delete', component: BookDeleteComponent, canActivate: [AuthGuard] },

            { path: 'books/:bookId/items/create', component: BookItemEditComponent, canActivate: [AuthGuard] },
            { path: 'books/:bookId/items/:itemId/edit', component: BookItemEditComponent, canActivate: [AuthGuard] },
            { path: 'books/:bookId/items/:itemId/delete', component: BookItemDeleteComponent, canActivate: [AuthGuard] },

            { path: 'members', component: MemberListComponent, canActivate: [AuthGuard] },
            { path: 'members/create', component: HomeComponent, canActivate: [AuthGuard] },
            { path: 'members/:userId', component: MemberDetailsComponent, canActivate: [AuthGuard] },
            { path: 'members/:userId/edit', component: MemberEditComponent, canActivate: [AuthGuard] },

            { path: 'rent', component: RentComponent, canActivate: [AuthGuard] },

            // otherwise redirect to home
            { path: '**', redirectTo: '' }
        ])
    ],
    providers: [
        AuthGuard,
        HttpClient,
        SecurityService
    ]
})
export class AppModuleShared {
}
