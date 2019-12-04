import { Component, OnInit } from '@angular/core';
import { TestService } from '../../../../services/test.service';
import { TestsApiDto } from 'src/app/models/test/tests-api-dto';

@Component({
    selector: 'app-test-list',
    templateUrl: './test-list.component.html',
    styleUrls: ['./test-list.component.css']
})
export class TestListComponent implements OnInit {
    public columnsToDisplay: string[] = ['id', 'title', 'description', 'timeLimitSeconds', 'lastModifiedDate', 'update', 'delete'];
    public testsApi: TestsApiDto = {} as TestsApiDto;
    public currentPageIndex = 0;
    public pageSize = 15;
    public pageSizeOptions: number[] = [this.pageSize, 10, 20];

    constructor(private testService: TestService) { }

    ngOnInit() {
        this.setTestsPage(this.currentPageIndex, this.pageSize);
    }

    public deleteTest(id: number) {
        this.testService.deleteTest(id).subscribe(() => {
            this.setTestsPage(this.testsApi.tests.length === 1
                ? ((this.currentPageIndex - 1) >= 0 ? (this.currentPageIndex - 1) : 0)
                : this.currentPageIndex,
                this.pageSize);
        });
    }

    public setTestsPage(pageIndex: number, pageSize: number) {
        this.currentPageIndex = pageIndex;
        this.testService.getTests(pageIndex, pageSize).subscribe(testsApiResp => this.testsApi = testsApiResp.body);
    }
}
