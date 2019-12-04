import { Component, OnInit, Input } from '@angular/core';
import { ResultTestDto } from 'src/app/models/test/result-test-dto';

@Component({
    selector: 'app-test',
    templateUrl: './test.component.html',
    styleUrls: ['./test.component.css']
})
export class TestComponent {
    @Input() test: ResultTestDto;
}
