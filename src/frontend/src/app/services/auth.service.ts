import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { AuthenticateRequest } from "../models/authenticateRequest";
import { AuthenticateResponse } from "../models/authenticateResponse";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  private static token: string;

  constructor(private http: HttpClient) {}

  public getToken(): string {
    return AuthService.token;
  }

  public async logIn(
    authData: AuthenticateRequest
  ): Promise<AuthenticateResponse> {
    const response = await this.http
      .post<AuthenticateResponse>(
        `${environment.apiBaseUrl}/users/authenticate`,
        authData
      )
      .toPromise();

      AuthService.token = response.token;
    return response;
  }
}
