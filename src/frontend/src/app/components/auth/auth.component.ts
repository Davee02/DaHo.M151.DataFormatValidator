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

    try {
      await this.authService.logIn(authData);
    } catch (error) {
      alert(error.error);
      return;
    }

    alert("Login was successful!");
  }

  logout(): void {
    this.authService.logOut();
  }
}
