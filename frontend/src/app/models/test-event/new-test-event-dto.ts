import { EventType } from './enums/event-type.enum';

export interface NewTestEventDto {
    sessionId: string;
    eventType: EventType;
    payload: string;
}
