import { Directive, Input, Output, EventEmitter, OnInit, OnChanges, OnDestroy } from '@angular/core';
import { Subject, timer, Observable } from 'rxjs';
import { switchMap, take, takeUntil, map } from 'rxjs/operators';

@Directive({
    selector: '[appCounter]'
})
export class CounterDirective implements OnInit, OnChanges, OnDestroy {
    @Input() counter: number;
    @Input() interval: number;
    @Output() value: EventEmitter<number> = new EventEmitter<number>();
    @Output() counterCompleted: EventEmitter<void> = new EventEmitter<void>();

    private counterSource: Subject<any> = new Subject<any>();
    private ngUnsubscribe = new Subject();

    constructor() { }

    ngOnInit() {
        this.counterSource.pipe(takeUntil(this.ngUnsubscribe)).subscribe(({ count, interval }) => {
            timer(0, interval).pipe(take(count)).subscribe(() => {
                this.value.emit(--count);
            },
                () => { },
                () => {
                    this.counterCompleted.emit();
                }
            );
        });

        this.counterSource.next({ count: this.counter, interval: this.interval });
    }

    ngOnChanges() {
        this.counterSource.next({ count: this.counter, interval: this.interval });
    }

    ngOnDestroy() {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
}
