import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class HttpInternalService {
    private baseUrl: string = environment.apiUrl;

    constructor(private http: HttpClient) { }

    public getRequest<T>(url: string, httpParams?: any): Observable<HttpResponse<T>> {
        return this.http.get<T>(this.baseUrl + url, {
            observe: 'response',
            params: httpParams,
            withCredentials: true
        });
    }

    public postRequest<T>(url: string, payload: object): Observable<HttpResponse<T>> {
        return this.http.post<T>(this.baseUrl + url, payload, {
            observe: 'response',
            withCredentials: true
        });
    }

    public putRequest<T>(url: string, payload: object): Observable<HttpResponse<T>> {
        return this.http.put<T>(this.baseUrl + url, payload, {
            observe: 'response',
            withCredentials: true
        });
    }

    public deleteRequest<T>(url: string, httpParams?: any): Observable<HttpResponse<T>> {
        return this.http.delete<T>(this.baseUrl + url, {
            observe: 'response',
            params: httpParams,
            withCredentials: true
        });
    }
}
