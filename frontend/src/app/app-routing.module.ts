import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TestListComponent } from './modules/tests/components/test-list/test-list.component';
import { TestCreateComponent } from './modules/tests/components/test-create/test-create.component';


const routes: Routes = [
    { path: '', redirectTo: 'tests', pathMatch: 'full' },
    { path: 'tests', component: TestListComponent },
    { path: 'tests/new', component: TestCreateComponent },
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
