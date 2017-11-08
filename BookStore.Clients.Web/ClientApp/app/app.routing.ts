import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from './guards/auth';
import { HomeComponent } from './components/home/home.component';
import { BookDetailsComponent } from './components/books/book.details.component';
import { BookListComponent } from './components/books/book.list.component';
import { BookEditComponent } from './components/books/book.edit.component';

const appRoutes: Routes = [
    { path: 'login', component: HomeComponent },

    { path: 'books', component: BookListComponent, canActivate: [AuthGuard] },
    { path: 'books/create', component: BookEditComponent, canActivate: [AuthGuard] },
    { path: 'books/:bookId', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'books/:bookId/edit', component: BookEditComponent, canActivate: [AuthGuard] },

    { path: 'books/:bookId/items/create', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'books/:bookId/items/:itemId/edit', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'books/:bookId/items/:itemId/delete', component: HomeComponent, canActivate: [AuthGuard] },

    { path: 'users', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'users/create', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'users/:userId', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'users/:userId/edit', component: HomeComponent, canActivate: [AuthGuard] },
    
    { path: 'rent', component: HomeComponent, canActivate: [AuthGuard] },

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);