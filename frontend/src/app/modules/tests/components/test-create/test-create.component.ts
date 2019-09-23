import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { TestService } from 'src/app/services/test.service';
import { NewTestDto } from 'src/app/models/test/new-test-dto';
import { CreatedTestDto } from 'src/app/models/test/created-test-dto';

@Component({
    selector: 'app-test-create',
    templateUrl: './test-create.component.html',
    styleUrls: ['./test-create.component.css']
})
export class TestCreateComponent implements OnInit {
    public newTest: NewTestDto = {} as NewTestDto;

    private deleteQuestionsForms: Subject<void> = new Subject<void>();
    private deleteUrlsForms: Subject<void> = new Subject<void>();
    private sendQuestions: Subject<CreatedTestDto> = new Subject<CreatedTestDto>();
    private sendUrlsAndDeleteForms: Subject<CreatedTestDto> = new Subject<CreatedTestDto>();

    constructor(private testService: TestService) { }

    ngOnInit() {
    }

    public sendTest() {
        this.newTest.authorId = 14101;

        const timeLimitSeconds: Date = new Date(0, 0, 0, 0, 0, 0);
        timeLimitSeconds.setSeconds(parseInt(this.newTest.timeLimitSeconds));
        this.newTest.timeLimitSeconds = timeLimitSeconds.toLocaleTimeString();

        this.testService.createTest(this.newTest).subscribe(respTest => {
            this.sendQuestions.next(respTest.body);
            this.sendUrlsAndDeleteForms.next(respTest.body);
            this.clearTest();
        });
    }

    public clearTestWithChildForms() {
        this.clearTest();
        this.deleteQuestionsForms.next();
        this.deleteUrlsForms.next();
    }

    private clearTest() {
        this.newTest = {} as NewTestDto;
    }
}
