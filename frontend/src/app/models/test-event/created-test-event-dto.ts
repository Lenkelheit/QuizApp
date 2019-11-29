import { EventType } from './enums/event-type.enum';

export interface CreatedTestEventDto {
    id: number;
    sessionId: string;
    startTime: Date;
    eventType: EventType;
    payload: string;
}
