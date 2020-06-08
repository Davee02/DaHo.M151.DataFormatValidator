import { Component, OnInit } from "@angular/core";
import { AuthService } from "src/app/services/auth.service";
import { AuthenticateRequest } from "src/app/models/authenticateRequest";

@Component({
  selector: "app-auth",
  templateUrl: "./auth.component.html",
  styleUrls: ["./auth.component.css"],
})
export class AuthComponent implements OnInit {
  public username: string;
  public password: string;

  constructor(private authService: AuthService) {}

  ngOnInit() {}

  async login(): Promise<void> {
    const authData: AuthenticateRequest = {
      username: this.username,
      password: this.password,
    };

    this.authService.logIn(authData);
  }
}
