import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ClipboardSnackBarComponent } from './clipboard-snack-bar.component';
import { SharedModule } from '../../shared.module';
import { MatSnackBarRef } from '@angular/material';

describe('ClipboardSnackBarComponent', () => {
    let component: ClipboardSnackBarComponent;
    let fixture: ComponentFixture<ClipboardSnackBarComponent>;
    const matSnackBarRefStub = jasmine.createSpyObj('MatSnackBarRef', ['dismiss']);

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [
                SharedModule
            ],
            providers: [
                { provide: MatSnackBarRef, useValue: matSnackBarRefStub }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ClipboardSnackBarComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should be dismissed', () => {
        const hostElement: HTMLElement = fixture.nativeElement;
        const button = hostElement.querySelector('button');

        button.click();

        fixture.whenStable().then(() => {
            expect(matSnackBarRefStub.dismiss).toHaveBeenCalled();
        });
    });
});
