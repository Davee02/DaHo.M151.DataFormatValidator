import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { DataFormat } from "../models/dataFormat";
import { environment } from "../../environments/environment";
import { ConvertFormatResponse } from '../models/convertFormatResponse';
import { ConvertFormatRequest } from '../models/convertFormatRequest';
import { ValidateFormatRequest } from '../models/validateFormatRequest';
import { ValidateFormatResponse } from '../models/validateFormatResponse';

@Injectable({
  providedIn: "root",
})
export class DataFormatService {
  constructor(private http: HttpClient) {}

  public getAllDataFormats(): Observable<DataFormat[]> {
    return this.http.get<DataFormat[]>(
      `${environment.apiBaseUrl}/dataformat/list`
    );
  }

  public convertDataFormat(requestData: ConvertFormatRequest): Observable<ConvertFormatResponse> {
    return this.http.post<ConvertFormatResponse>(
      `${environment.apiBaseUrl}/dataformat/convert`,
      requestData
    );
  }

  public validateDataFormat(requestData: ValidateFormatRequest): Observable<ValidateFormatResponse> {
    return this.http.post<ValidateFormatResponse>(
      `${environment.apiBaseUrl}/dataformat/validate`,
      requestData
    );
  }

  public validateDataFormatWithSchema(requestData: ValidateFormatRequest, schemaName: string): Observable<ValidateFormatResponse> {
    return this.http.post<ValidateFormatResponse>(
      `${environment.apiBaseUrl}/dataformat/validate/${schemaName}`,
      requestData
    );
  }
}
