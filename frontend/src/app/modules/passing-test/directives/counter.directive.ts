import { Directive, Input, Output, EventEmitter, OnInit, OnChanges, OnDestroy } from '@angular/core';
import { Subject, timer, Subscription } from 'rxjs';
import { take } from 'rxjs/operators';

@Directive({
    selector: '[appCounter]'
})
export class CounterDirective implements OnInit, OnChanges, OnDestroy {
    private subscription: Subscription = new Subscription();
    private counterSource: Subject<any> = new Subject<any>();

    @Input() counter: number;
    @Input() interval: number;
    @Output() value: EventEmitter<number> = new EventEmitter<number>();
    @Output() counterCompleted: EventEmitter<void> = new EventEmitter<void>();

    ngOnInit() {
        this.subscription.add(
            this.counterSource.subscribe(({ count, interval }) => {
                timer(0, interval).pipe(take(count)).subscribe(() => {
                    this.value.emit(--count);
                },
                    () => { },
                    () => {
                        this.counterCompleted.emit();
                    }
                );
            })
        );

        this.counterSource.next({ count: this.counter, interval: this.interval });
    }

    ngOnChanges() {
        this.counterSource.next({ count: this.counter, interval: this.interval });
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}
