import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TestListComponent } from './modules/tests/components/test-list/test-list.component';
import { TestCreateComponent } from './modules/tests/components/test-create/test-create.component';
import { TestEditComponent } from './modules/tests/components/test-edit/test-edit.component';
import { UrlListComponent } from './modules/urls/components/url-list/url-list.component';
import { UrlCreateComponent } from './modules/urls/components/url-create/url-create.component';
import { UrlEditComponent } from './modules/urls/components/url-edit/url-edit.component';
import { TestPassComponent } from './modules/passing-test/components/test-pass/test-pass.component';
import { TestResultComponent } from './modules/test-results/components/test-result/test-result.component';
import { TestResultListComponent } from './modules/test-results/components/test-result-list/test-result-list.component';
import { UserLoginComponent } from './modules/authentication/components/user-login/user-login.component';
import { AuthGuard } from './services/auth.guard';
import { UserSignupComponent } from './modules/authentication/components/user-signup/user-signup.component';
import { PageNotFoundComponent } from './shared/components/page-not-found/page-not-found.component';

const routes: Routes = [
    { path: '', redirectTo: 'tests', pathMatch: 'full' },
    { path: 'login', component: UserLoginComponent },
    { path: 'register', component: UserSignupComponent },

    { path: 'tests', component: TestListComponent, canActivate: [AuthGuard] },
    { path: 'tests/new', component: TestCreateComponent, canActivate: [AuthGuard] },
    { path: 'tests/:id', component: TestEditComponent, canActivate: [AuthGuard] },
    { path: 'urls', component: UrlListComponent, canActivate: [AuthGuard] },
    { path: 'urls/new', component: UrlCreateComponent, canActivate: [AuthGuard] },
    { path: 'urls/:id', component: UrlEditComponent, canActivate: [AuthGuard] },

    { path: 'passing-test/:id', component: TestPassComponent },
    { path: 'passing-test/test-result/:id', component: TestResultComponent },

    { path: 'test-results', component: TestResultListComponent, canActivate: [AuthGuard] },
    { path: 'test-results/:id', component: TestResultComponent, canActivate: [AuthGuard] },

    { path: '404', component: PageNotFoundComponent },
    { path: '**', redirectTo: '404' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    providers: [AuthGuard]
})
export class AppRoutingModule { }
