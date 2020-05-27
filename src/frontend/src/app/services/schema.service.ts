import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DataSchema } from '../models/dataSchema';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SchemaService {
  constructor(private http: HttpClient) {}

  public getAllSchemas(): Observable<DataSchema[]> {
    return this.http.get<DataSchema[]>(
      `${environment.apiBaseUrl}/schemas`
    );
  }

  public getSchema(schemaName: string): Observable<DataSchema> {
    return this.http.get<DataSchema>(
      `${environment.apiBaseUrl}/schemas/${schemaName}`
    );
  }

  public createSchema(schema: DataSchema): Observable<any> {
    return this.http.post(
      `${environment.apiBaseUrl}/schemas/`,
      schema
    );
  }

  public deleteSchema(schemaName: string): Observable<any> {
    return this.http.delete<DataSchema>(
      `${environment.apiBaseUrl}/schemas/${schemaName}`
    );
  }
}
