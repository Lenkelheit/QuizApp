import { TestBed, async } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { Component } from '@angular/core';

@Component({ selector: 'app-top-bar', template: '' })
class TopBarStubComponent { }

@Component({ selector: 'router-outlet', template: '' })
class RouterOutletStubComponent { }

@Component({ selector: 'app-footer', template: '' })
class FooterStubComponent { }

describe('AppComponent', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [
                AppComponent,
                TopBarStubComponent,
                RouterOutletStubComponent,
                FooterStubComponent
            ]
        }).compileComponents();
    }));

    it('should create the app', () => {
        const fixture = TestBed.createComponent(AppComponent);
        const app = fixture.componentInstance;
        expect(app).toBeTruthy();
    });
});
