import { Component, OnInit } from '@angular/core';
import { TestService } from '../../../../services/test.service';
import { TestDto } from '../../../../models/test/test-dto';

@Component({
    selector: 'app-test-list',
    templateUrl: './test-list.component.html',
    styleUrls: ['./test-list.component.css']
})
export class TestListComponent implements OnInit {
    public columnsToDisplay: string[] = ['id', 'title', 'description', 'timeLimitSeconds', 'lastModifiedDate', 'update', 'delete'];
    public tests: TestDto[] = [];

    constructor(private testService: TestService) { }

    ngOnInit() {
        this.getTests();
    }

    public deleteTest(id: number) {
        this.testService.deleteTest(id).subscribe(() => this.getTests());
    }

    private getTests() {
        this.testService.getTests().subscribe(resp => this.tests = resp.body);
    }
}
