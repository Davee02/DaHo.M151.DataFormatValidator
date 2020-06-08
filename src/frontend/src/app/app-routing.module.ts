import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ValidateComponent } from "./components/validate/validate.component";
import { ConvertComponent } from "./components/convert/convert.component";
import { SchemaComponent } from "./components/schema/schema.component";
import { AuthComponent } from './components/auth/auth.component';

const routes: Routes = [
  { path: "auth", component: AuthComponent },
  { path: "", component: AuthComponent },
  { path: "validate", component: ValidateComponent },
  { path: "convert", component: ConvertComponent },
  { path: "schema", component: SchemaComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
