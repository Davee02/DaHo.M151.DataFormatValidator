import { Component, OnInit } from "@angular/core";
import { DataFormatService } from "./services/data-format.service";
import { SchemaService } from "./services/schema.service";
import { DataFormat } from './models/dataFormat';

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"],
})
export class AppComponent implements OnInit {
  public allDataFormats: DataFormat[];

  constructor(
    private dataFormatService: DataFormatService,
    private schemaService: SchemaService
  ) {}

  async ngOnInit(): Promise<void> {
    this.allDataFormats = await this.dataFormatService.getAllDataFormats().toPromise();
  }
}
