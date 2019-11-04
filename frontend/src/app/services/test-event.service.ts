import { Injectable } from '@angular/core';
import { HttpInternalService } from '../http-internal.service';
import { NewTestEventDto } from '../models/test-event/new-test-event-dto';
import { CreatedTestEventDto } from '../models/test-event/created-test-event-dto';

@Injectable({
    providedIn: 'root'
})
export class TestEventService {
    public routePrefix = '/api/test-event';

    constructor(private httpService: HttpInternalService) { }

    public generateSessionId() {
        return this.httpService.getRequest<string>(`${this.routePrefix}/session-id`);
    }

    public createTestEvent(testEvent: NewTestEventDto) {
        return this.httpService.postRequest<CreatedTestEventDto>(`${this.routePrefix}`, testEvent);
    }
}
