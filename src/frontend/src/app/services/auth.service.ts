import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { AuthenticateRequest } from "../models/authenticateRequest";
import { AuthenticateResponse } from "../models/authenticateResponse";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  private static readonly TOKEN_KEY = "bearerToken";

  constructor(private http: HttpClient) {}

  public getToken(): string {
    return localStorage.getItem(AuthService.TOKEN_KEY);
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

    localStorage.setItem(AuthService.TOKEN_KEY, response.token);
    return response;
  }

  public logOut(): void {
    localStorage.removeItem(AuthService.TOKEN_KEY);
  }
}
