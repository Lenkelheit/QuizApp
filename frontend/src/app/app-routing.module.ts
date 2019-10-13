import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TestListComponent } from './modules/tests/components/test-list/test-list.component';
import { TestCreateComponent } from './modules/tests/components/test-create/test-create.component';
import { TestEditComponent } from './modules/tests/components/test-edit/test-edit.component';
import { UrlListComponent } from './modules/urls/components/url-list/url-list.component';
import { UrlCreateComponent } from './modules/urls/components/url-create/url-create.component';
import { UrlEditComponent } from './modules/urls/components/url-edit/url-edit.component';

const routes: Routes = [
    { path: '', redirectTo: 'tests', pathMatch: 'full' },
    { path: 'tests', component: TestListComponent },
    { path: 'tests/new', component: TestCreateComponent },
    { path: 'tests/:id', component: TestEditComponent },
    { path: 'urls', component: UrlListComponent },
    { path: 'urls/new', component: UrlCreateComponent },
    { path: 'urls/:id', component: UrlEditComponent },
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
