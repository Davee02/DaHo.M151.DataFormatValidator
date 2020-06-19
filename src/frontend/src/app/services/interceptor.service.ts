import { Injectable } from "@angular/core";
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse,
} from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { AuthService } from "./auth.service";
@Injectable({
  providedIn: "root",
})
export class InterceptorService implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  handleError(error: HttpErrorResponse) {
    console.warn(error);

    if (error.status === 401 || error.status === 403) {
      alert(
        "You do not have enough rights or aren't even logged in to perform this action! Please use the auth section to log in!"
      );
    }

    return throwError(error);
  }

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const token = this.authService.getToken();

    const clonedRequest = req.clone({
      headers: req.headers.set("Authorization", `Bearer ${token}`),
    });

    return next.handle(clonedRequest).pipe(catchError(this.handleError));
  }
}
